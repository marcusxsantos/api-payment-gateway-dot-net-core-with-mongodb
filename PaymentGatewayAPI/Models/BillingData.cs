using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PaymentGatewayAPI.Models
{
	public class BillingData
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string ID { get; set; }
		[BsonElement("Type")]
		public int Type { get; set; }
		[BsonElement("BirthDate")]
		public string BirthDate { get; set; }
		[BsonElement("Gender")]
		public string Gender { get; set; }
		[BsonElement("Name")]
		public string Name { get; set; }
		[BsonElement("Email")]
		public string Email { get; set; }
	}
}
