using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Clear.Sale.Mock.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Clear.Sale.Mock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
		[HttpPost]
		public IActionResult Post([FromBody]RequestSend requestSend)
		{
			if (requestSend.LoginToken != "ded6a687514227ff822d40bd397f30f5ae9132487ad6c846599131c740d784f0")
				return Unauthorized();

			string[] status = new string[2] { "APA", "FRD" };
			Random ranSts = new Random();
			string mystatus = status[ranSts.Next(0, status.Length)];

			int[] score = new int[5] { 1, 2, 3, 4, 5 };
			Random ranScore = new Random();
			int myscore = score[ranScore.Next(0, score.Length)];

			var response = new {
				Orders = new {
					ID = Guid.NewGuid().ToString(),
					Status = mystatus,
					Score = myscore
				}
			};

			return Ok(response);
		}
	}
}