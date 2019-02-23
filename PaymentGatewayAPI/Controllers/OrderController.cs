using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PaymentGatewayAPI.DAO.Interfaces;
using PaymentGatewayAPI.Models;
using PaymentGatewayAPI.Models.Interfaces;

namespace PaymentGatewayAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
		public readonly IOrderDao _orderDao;

		public OrderController(IOrderDao orderDao)
		{
			_orderDao = orderDao;
		}

		[Authorize("Bearer")]
		[HttpPost]
		[Route("send")]
		public IActionResult Send([FromBody]Order order)
		{
			var user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("User").ToString());

			order.User = user;

			bool result = _orderDao.Save(order);
			if (result)
				return Ok(new
				{
					message = "Registrado com sucesso",
					status = "success"
				});
			else
				return BadRequest(new
				{
					message = "Registro não realizado.",
					status = "fail"
				});
		}

		[Authorize("Bearer")]
		[HttpPost]
		public IActionResult Get()
		{
			IList<IOrder> orders = _orderDao.
		}
	}
}