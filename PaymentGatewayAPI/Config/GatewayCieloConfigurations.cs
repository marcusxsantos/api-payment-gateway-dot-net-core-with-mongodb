using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Config
{
	public class GatewayCieloConfigurations
	{
		public string Url { get; set; }
		public string MerchantId { get; set; }
		public string MerchantKey { get; set; }
		public string SalesApi { get; set; }
	}
}
