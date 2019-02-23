using System.Collections.Generic;
using PaymentGatewayAPI.Models.Interfaces;

namespace PaymentGatewayAPI.DAO.Interfaces
{
	public interface IOrderDao
	{
		IList<IOrder> GetAll();
		bool Save(IOrder order);
	}
}
