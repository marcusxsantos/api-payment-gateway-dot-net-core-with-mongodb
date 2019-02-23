using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Models.Interfaces
{
	interface IAcquirer
	{
		int ID { get; set; }
		string Name { get; set; }
	}
}
