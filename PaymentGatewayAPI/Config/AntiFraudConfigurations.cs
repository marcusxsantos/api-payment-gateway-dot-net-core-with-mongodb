using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Config
{
	public class AntiFraudConfigurations
	{
		public string Url { get; set; }
		public string Port { get; set; }
		public string LoginApi { get; set; }
		public string OrderApi { get; set; }
	}
}
