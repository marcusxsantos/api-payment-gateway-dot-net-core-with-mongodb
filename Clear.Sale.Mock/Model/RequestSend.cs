using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clear.Sale.Mock.Model
{
	public class RequestSend
	{
		public string ApiKey { get; set; }
		public string LoginToken { get; set; }
		public Order Order { get; set; }
	}
}
