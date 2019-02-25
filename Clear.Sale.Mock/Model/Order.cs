using System;

namespace Clear.Sale.Mock.Model
{
	public class Order 
	{		
		public string ID { get; set; }
		public DateTime Date { get; set; }
		public string Email { get; set; }
		public double TotalItems { get; set; }
		public double TotalOrder { get; set; }
		public Payment[] Payment { get; set; }
		public OrderItem[] Items { get; set; }
		public BillingData BillingData { get; set; }
	}
}
