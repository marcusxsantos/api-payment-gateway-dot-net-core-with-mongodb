using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Clear.Sale.Mock.Model.Auth;

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

		[HttpGet]
		[Route("login")]
		public IActionResult Login([FromBody]RequestAuth request)		
		{
			Login login = request.Login;

			if (login.IsValid())
			{
				_token.Value = "meLJgIWJq06fynaqEBMk69AkHTj7lXV0";
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
			//return new string[] { "value1-Logout1", "value2-Logout2" };
		}
	}
}