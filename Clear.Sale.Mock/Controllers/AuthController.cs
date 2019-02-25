using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Clear.Sale.Mock.Model.Auth;
using System;

namespace Clear.Sale.Mock.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
		private readonly IResponseAuth _response;
		private readonly IToken _token;

		public AuthController(IResponseAuth response, IToken token)
		{
			_response = response;
			_token = token;
		}

		[HttpPost]
		[Route("login")]
		public IActionResult Login([FromBody]RequestAuth request)		
		{
			Login login = request.Login;

			if (login.IsValid())
			{
				//Mock
				_token.Value = "ded6a687514227ff822d40bd397f30f5ae9132487ad6c846599131c740d784f0";
				_token.ExpirationDate = new System.DateTime(2019, 2, 28, 23, 59, 59);

				_response.Token = _token;

				return Ok(_response);
			}
			else
			{
				return Unauthorized();
			}
		}
		[Route("logout")]
		public void Logout([FromBody]RequestAuth request)
		{
			SignOut();
		}
	}
}