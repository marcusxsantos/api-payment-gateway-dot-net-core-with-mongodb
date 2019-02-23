using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentGatewayAPI.Models.Interfaces;

namespace PaymentGatewayAPI.Models
{
	public class Acquirer : IAcquirer
	{
		public int ID { get; set; }
		public string Name { get; set; }
	}
}
