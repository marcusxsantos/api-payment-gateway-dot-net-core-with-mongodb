using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentGatewayAPI.Models.Interfaces;

namespace PaymentGatewayAPI.DAO.Interfaces
{
	public interface IUserDao
	{
		IUser FindByApiKey(string ApiKey);
		bool Save(IUser user);
	}
}
