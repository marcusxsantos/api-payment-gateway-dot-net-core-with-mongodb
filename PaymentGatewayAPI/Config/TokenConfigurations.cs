using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentGatewayAPI.Config.Interfaces;

namespace PaymentGatewayAPI.Config
{
	public class TokenConfigurations : ITokenConfigurations
	{
		public string Audience { get; set; }
		public string Issuer { get; set; }
		public int Seconds { get; set; }
	}
}
