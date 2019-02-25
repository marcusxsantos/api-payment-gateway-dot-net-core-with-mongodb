using Microsoft.AspNetCore.Mvc;

namespace Clear.Sale.Mock.Controllers
{
	[Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
		// GET api/values
		[HttpGet]
		public ActionResult<string> Get()
		{
			return "Api Clear Sale running";
		}
		
	}
}