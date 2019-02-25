using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Models.GatewayCielo
{
	public class Link
	{
		public string Method { get; set; }
		public string Rel { get; set; }
		public string Href { get; set; }
	}
}
