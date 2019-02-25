using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Models
{
	public class OrderStatus
	{
		public string ID { get; set; }
		public string Status { get; set; }
		public double Score { get; set; }
	}
}
