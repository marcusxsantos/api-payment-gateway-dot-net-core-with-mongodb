using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clear.Sale.Mock.Model
{
	public class Payment
	{
		public DateTime Date { get; set; }
		public int Type { get; set; }
		public string CardNumber { get; set; }
		public string CardHolderName { get; set; }
		public string CardExpirationDate { get; set; }
		public double Amount { get; set; }
		public int PaymentTypeID { get; set; }
		public int CardType { get; set; }
		public string CardBin { get; set; }
	}
}
