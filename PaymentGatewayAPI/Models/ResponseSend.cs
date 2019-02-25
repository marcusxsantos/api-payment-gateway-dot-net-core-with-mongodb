using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Models
{
	public class ResponseSend
	{
		public OrderStatus Orders { get; set; }
		public string TransactionID { get; set; }
	}
}
