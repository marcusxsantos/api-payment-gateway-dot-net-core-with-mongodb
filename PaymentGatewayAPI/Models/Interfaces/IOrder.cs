using System;

namespace PaymentGatewayAPI.Models.Interfaces
{
	public interface IOrder
	{
		string ID { get; set; }
		DateTime Date { get; set; }
		string Email{ get; set; }
		double TotalItems{ get; set; }
		double TotalOrder { get; set; }		
	}
}
