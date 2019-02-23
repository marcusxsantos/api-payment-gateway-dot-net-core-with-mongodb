using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using PaymentGatewayAPI.Models.Interfaces;

namespace PaymentGatewayAPI.Models
{
	public class OrderItem : IOrderItem
	{
		[BsonElement("ProductId")]
		public string ProductId { get; set; }
		[BsonElement("ProductTitle")]
		public string ProductTitle { get; set; }
		[BsonElement("Price")]
		public double Price { get; set; }
		[BsonElement("Category")]
		public string Category { get; set; }
		[BsonElement("Quantity")]
		public double Quantity { get; set; }
	}
}
