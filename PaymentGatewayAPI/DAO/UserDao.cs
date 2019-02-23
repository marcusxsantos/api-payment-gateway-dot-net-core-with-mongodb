using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentGatewayAPI.DAO.Interfaces;
using PaymentGatewayAPI.Models;
using PaymentGatewayAPI.Models.Interfaces;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace PaymentGatewayAPI.DAO
{
	public class UserDao : IUserDao
	{
		private readonly IMongoCollection<User> _users;

		public UserDao(IConfiguration config)
		{
			var client = new MongoClient(config.GetConnectionString("PaymentGatewayAPIDb"));
			var database = client.GetDatabase("paymentgatewayapibd");
			_users = database.GetCollection<User>("Users");
		}

		private List<User> getUsers()
		{
			return _users.Find(user => true).ToList(); ;
		}

		public IUser FindByApiKey(string ApiKey)
		{
			List<User> users = getUsers();
			return users.Where(u => u.ApiKey.Equals(ApiKey)).FirstOrDefault();
		}

		public bool Save(IUser user)
		{
			_users.InsertOne((User)user);
			return true;
		}
	}
}
