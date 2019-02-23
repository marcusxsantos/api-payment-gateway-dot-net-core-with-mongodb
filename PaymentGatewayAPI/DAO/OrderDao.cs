using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PaymentGatewayAPI.DAO.Interfaces;
using PaymentGatewayAPI.Models;
using PaymentGatewayAPI.Models.Interfaces;

namespace PaymentGatewayAPI.DAO
{
	public class OrderDao : IOrderDao
	{
		private readonly IMongoCollection<Order> _orders;

		public OrderDao(IConfiguration config)
		{
			var client = new MongoClient(config.GetConnectionString("PaymentGatewayAPIDb"));
			var database = client.GetDatabase("paymentgatewayapibd");
			_orders = database.GetCollection<Order>("Orders");
		}

		public IList<IOrder> GetAll()
		{
			return (IList<IOrder>)_orders.Find(order => true).ToList();
		}

		public bool Save(IOrder order)
		{
			_orders.InsertOne((Order)order);
			return true;
		}
	}
}
