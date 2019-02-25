using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Models.GatewayCielo
{
	public class GatewayCielo
	{
		public string MerchantOrderId { get; set; }
		public Customer Customer { get; set; }
		public Payment Payment { get; set; }
	}
}
