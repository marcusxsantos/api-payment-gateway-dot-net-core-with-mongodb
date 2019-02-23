using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Models.Interfaces
{
	public interface IUser
	{
		string Id { get; set; }
		string Name { get; set; }
		string IdetificationNumber { get; set; }
		string Email { get; set; }
		string ApiKey { get; set; }
		bool HasAntiFraud { get; set; }
	}
}
