using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PaymentGatewayAPI.Models.Interfaces;

namespace PaymentGatewayAPI.Models
{
	public class Order : IOrder
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; }
		[BsonElement("IdentificationCode")]
		public string IdentificationCode { get; set; }
		[BsonElement("Date")]
		public DateTime Date { get; set; }
		[BsonElement("Email")]
		public string Email { get; set; }
		[BsonElement("TotalItems")]
		public double TotalItems { get; set; }
		[BsonElement("TotalOrder")]
		public double TotalOrder { get; set; }
		public Payment[] Payment { get; set; }
		public OrderItem[] Items { get; set; }
		public User User { get; set; }
		public BillingData BillingData { get; set; }
	}
}
