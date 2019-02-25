using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PaymentGatewayAPI.DAO.Interfaces;
using PaymentGatewayAPI.Models;
using PaymentGatewayAPI.Models.Interfaces;
using PaymentGatewayAPI.Config;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using PaymentGatewayAPI.Util;
using System.Net;
using PaymentGatewayAPI.Util.Enums;
using PaymentGatewayAPI.Models.GatewayCielo;

namespace PaymentGatewayAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
		public readonly IOrderDao _orderDao;

		public OrderController(IOrderDao orderDao, IConfiguration Configuration)
		{
			_orderDao = orderDao;
		}

		[Authorize("Bearer")]
		[HttpPost]
		[Route("send")]
		public async Task<IActionResult> Send([FromBody]Order order, 
			[FromServices]AntiFraudConfigurations antiFraudConfigurations,
			[FromServices]GatewayCieloConfigurations gatewayCieloConfigurations)
		{
			var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User").ToString());
			var urlAntiFraudApiLogin = antiFraudConfigurations.Url + (antiFraudConfigurations.Port != string.Empty ? ":" + antiFraudConfigurations.Port : "") + antiFraudConfigurations.LoginApi;
			var urlAntiFraudApiOrder = antiFraudConfigurations.Url + (antiFraudConfigurations.Port != string.Empty ? ":" + antiFraudConfigurations.Port : "") + antiFraudConfigurations.OrderApi;

			eAntiFraudSatus statusAntiFraud = eAntiFraudSatus.NotRequired;

			if (user.HasAntiFraud)
			{				
				var objJson = JsonConvert.SerializeObject(new {
					Login = new {
						ApiKey = user.AntiFraudApiKey,
						ClientID = user.AntiFraudClientID,
						ClientSecret = user.AntiFraudClientSecret
					}
				});
				
				using (var client = new HttpClient())
				{
					client.DefaultRequestHeaders.Accept.Clear();
					client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

					HttpResponseMessage response = await client.PostAsync(urlAntiFraudApiLogin, new StringContent(objJson, Encoding.UTF8, "application/json"));
					if (response.StatusCode == HttpStatusCode.OK)
					{
						response.EnsureSuccessStatusCode();
						string responseBody = await response.Content.ReadAsStringAsync();

						var obj = JsonConvert.DeserializeObject<AntiFraudAuthToken>(responseBody);

						var objJsonOrder = JsonConvert.SerializeObject(new
						{
							ApiKey = user.AntiFraudApiKey,
							LoginToken = obj.Token.value,
							Order = order
						});

						HttpResponseMessage responseOrder = await client.PostAsync(urlAntiFraudApiOrder, new StringContent(objJsonOrder, Encoding.UTF8, "application/json"));

						if (responseOrder.StatusCode == HttpStatusCode.OK)
						{
							responseOrder.EnsureSuccessStatusCode();
							string responseBodyOrder = await responseOrder.Content.ReadAsStringAsync();

							var objCheckFraud = JsonConvert.DeserializeObject<ResponseSend>(responseBodyOrder);

							if (objCheckFraud.Orders.Status != "APA")
								return BadRequest(new
								{
									message = "Fluxo de pagamento deverá ser interrompido - Codigo:" + objCheckFraud.Orders.Status,									
									status = "fail",
									StatusCode = HttpStatusCode.BadGateway
								});
							else
								statusAntiFraud = eAntiFraudSatus.Valid;
						}else
							return BadRequest(new
							{
								message = "Erro na requisição",
								status = "fail",
								StatusCode = HttpStatusCode.BadGateway
							});
					}
					else
					{
						return BadRequest(new
						{
							message = "Registro não realizado.",
							status = "fail",
							StatusCode = HttpStatusCode.BadGateway
						});
					}
				}
			}
			
			bool result = false;

			GatewayCielo objPay = null;

			if (statusAntiFraud == eAntiFraudSatus.NotRequired || statusAntiFraud == eAntiFraudSatus.Valid)
			{
				order.User = user;
				order.IdentificationCode = Guid.NewGuid().ToString();
				result = _orderDao.Save(order);

				if (result)
				{
					var urlGateWayCielo = gatewayCieloConfigurations.Url + gatewayCieloConfigurations.SalesApi;

					for (int i = 0; i < order.Payment.Length; i++)
					{
						var objJSonCielo = JsonConvert.SerializeObject(new
						{
							MerchantOrderId = order.IdentificationCode,
							Customer = new
							{
								Name = order.BillingData.Name
							},
							Payment = new
							{
								Type = "CreditCard",
								Amount = order.Payment[i].Amount,
								Installments = order.Payment[i].QtyInstallments,
								SoftDescriptor = user.Name,
								CreditCard = new
								{
									CardNumber = order.Payment[i].CardNumber,
									Holder = order.Payment[i].CardHolderName,
									ExpirationDate = order.Payment[i].CardExpirationDate,
									SecurityCode = order.Payment[i].SecurityCode,
									Brand = DefineCardBrandCielo(order.Payment[i].CardType)
								}
							}
						});

						using (var clientCielo = new HttpClient())
						{
							clientCielo.DefaultRequestHeaders.Accept.Clear();
							clientCielo.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
							clientCielo.DefaultRequestHeaders.Add("MerchantId", gatewayCieloConfigurations.MerchantId);
							clientCielo.DefaultRequestHeaders.Add("MerchantKey", gatewayCieloConfigurations.MerchantKey);
							clientCielo.DefaultRequestHeaders.Add("RequestId", "");

							HttpResponseMessage response = await clientCielo.PostAsync(urlGateWayCielo, new StringContent(objJSonCielo, Encoding.UTF8, "application/json"));

							if (response.StatusCode == HttpStatusCode.Created)
							{
								response.EnsureSuccessStatusCode();
								string responseBody = await response.Content.ReadAsStringAsync();

								objPay = JsonConvert.DeserializeObject<GatewayCielo>(responseBody);
							}
							else {
								BadRequest(new
								{
									message = "Pagamento não registrado.",
									status = "fail",
									StatusCode = HttpStatusCode.NotAcceptable
								});
							}
						}
					}
				}
			}
			else
			{
				BadRequest(new
				{
					message = "Registro não realizado - Falha Anti Fraude.",
					status = "fail",
					StatusCode = HttpStatusCode.NotAcceptable
				});
			}

			if (result)
				return Ok(new
				{					
					message = "Registrado com sucesso",
					status = "success",
					orderId = order.IdentificationCode,
					Payment = objPay
				});
			else
				return BadRequest(new
				{
					message = "Registro não realizado.",
					status = "fail",
					StatusCode = HttpStatusCode.NotAcceptable
				});
		}

		[Authorize("Bearer")]
		[HttpGet]
		[Route("list")]
		public IActionResult Get()
		{
			List<Order> ordersGet = (List<Order>)_orderDao.GetAll();

			if (ordersGet != null)
				return Ok(new
				{
					orders = ordersGet,
					message = "Lista de Orders.",
					status = "success"
				});
			else
				return BadRequest(new
				{
					message = "Nenhum registro encontrado.",
					status = "fail"
				});
		}

		private string DefineCardBrandCielo(int cardType)
		{
			switch (cardType)
			{
				case 1: return   "Diners";
				case 2: return   "MasterCard";
				case 3: return   "Visa";
				case 4: return   "Others";
				case 5: return   "American Express";
				case 6: return   "HiperCard";
				case 7: return   "Aura";
				default: return "";
			}
		}
	}
}