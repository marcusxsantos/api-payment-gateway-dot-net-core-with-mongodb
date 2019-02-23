using System;
using System.ComponentModel.DataAnnotations;
using PaymentGatewayAPI.Models.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PaymentGatewayAPI.Models
{
	public class User : IUser
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		[Required]
		[BsonElement("Name")]
		public string Name { get; set; }
		[Required]
		[BsonElement("IdetificationNumber")]
		public string IdetificationNumber { get; set; }
		[Required]
		[BsonElement("Email")]
		public string Email { get; set; }
		[BsonElement("ApiKey")]
		public string ApiKey { get; set; }
		[Required]
		[BsonElement("HasAntiFraud")]
		public bool HasAntiFraud { get; set; }
		[BsonElement("AntiFraudApiKey")]
		public string AntiFraudApiKey { get; set; }
		[BsonElement("AntiFraudClientID")]
		public string AntiFraudClientID { get; set; }
		[BsonElement("AntiFraudClientSecret")]
		public string AntiFraudClientSecret { get; set; }
		
		public bool CheckAntiFraudParams()
		{
			if (AntiFraudApiKey != null && AntiFraudApiKey != string.Empty &&
				AntiFraudClientID != null && AntiFraudClientID != string.Empty &&
				AntiFraudClientSecret != null && AntiFraudClientSecret != string.Empty)
				return true;
			else
				return false;
		}

		public string GenerateApiKey()
		{
			ApiKey = Guid.NewGuid().ToString();
			return ApiKey;
		}		
	}
}
