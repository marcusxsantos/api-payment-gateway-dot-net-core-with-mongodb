using System.Collections.Generic;
using PaymentGatewayAPI.Models.Interfaces;

namespace PaymentGatewayAPI.DAO.Interfaces
{
	public interface IOrderDao
	{
		object GetAll();
		bool Save(IOrder order);
	}
}
