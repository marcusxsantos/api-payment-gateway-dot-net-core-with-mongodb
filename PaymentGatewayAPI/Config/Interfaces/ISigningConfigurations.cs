using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace PaymentGatewayAPI.Config.Interfaces
{
	public interface ISigningConfigurations
	{
		SecurityKey Key { get; }
		SigningCredentials SigningCredentials { get; }
	}
}
