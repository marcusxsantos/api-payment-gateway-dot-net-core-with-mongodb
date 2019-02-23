using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Config.Interfaces
{
	public interface ITokenConfigurations
	{
		string Audience { get; set; }
		string Issuer { get; set; }
		int Seconds { get; set; }
	}
}
