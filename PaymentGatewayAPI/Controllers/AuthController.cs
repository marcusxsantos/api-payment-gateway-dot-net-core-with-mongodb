
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PaymentGatewayAPI.Config;
using PaymentGatewayAPI.DAO.Interfaces;
using PaymentGatewayAPI.Models;
using PaymentGatewayAPI.Models.Interfaces;

namespace PaymentGatewayAPI.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
		public IUser _user;
		public readonly IUserDao _userDao;

		public AuthController(IUserDao userDao, IUser user)
		{
			_userDao = userDao;
			_user = user;
		}

		[AllowAnonymous]
		[HttpPost]
		[Route("register")]
		public IActionResult Register([FromBody]User user)
		{
			bool result = false;

			if (user.HasAntiFraud && !user.CheckAntiFraudParams())	
				return BadRequest(new
				{
					message = "Os Dados Anti Fraude não informados corretamente.",
					status = "fail"
				});			
		
			string apiKeyGen = user.GenerateApiKey();
			user.ApiKey = apiKeyGen;

			result = _userDao.Save(user);

			if (result)
				return Ok(new
				{
					ApiKey = apiKeyGen,
					message = "Registrado com sucesso.",
					status = "success"
				});
			else 
				return BadRequest(new
				{
					message = "Registro não realizado.",
					status = "fail"
				});					
		}		
		[AllowAnonymous]
		[HttpPost]
		[Route("login")]
		public IActionResult Login(string apiKey, 
			[FromServices]SigningConfigurations signingConfigurations,
			[FromServices]TokenConfigurations tokenConfigurations)
		{
			bool credenciaisValidas = false;

			if (apiKey != null && !String.IsNullOrWhiteSpace(apiKey))
			{
				_user = _userDao.FindByApiKey(apiKey);
				credenciaisValidas = _user != null;
			}

			if (credenciaisValidas)
			{
				ClaimsIdentity identity = new ClaimsIdentity(
					new GenericIdentity(_user.ApiKey, "Login"),
					new[] {
						new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
						new Claim(JwtRegisteredClaimNames.UniqueName, _user.ApiKey)
					}
				);

				DateTime dataCriacao = DateTime.Now;
				DateTime dataExpiracao = dataCriacao +
					TimeSpan.FromSeconds(tokenConfigurations.Seconds);

				var handler = new JwtSecurityTokenHandler();
				var securityToken = handler.CreateToken(new SecurityTokenDescriptor
				{
					Issuer = tokenConfigurations.Issuer,
					Audience = tokenConfigurations.Audience,
					SigningCredentials = signingConfigurations.SigningCredentials,
					Subject = identity,
					NotBefore = dataCriacao,
					Expires = dataExpiracao
				});
				var token = handler.WriteToken(securityToken);

				HttpContext.Session.SetString("User", JsonConvert.SerializeObject(_user));

				return Ok(new
				{
					authenticated = true,
					created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
					expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
					accessToken = token
				});
			}
			else
			{
				return Unauthorized();
			}
		}
	}
}