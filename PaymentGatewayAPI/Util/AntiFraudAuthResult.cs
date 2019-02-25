using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Util
{
	public class AntiFraudAuthToken
	{
		public Token Token { get; set; }
	}

	public class Token {
		public string value { get; set; }
		public string expirationDate { get; set; }
	}
}
