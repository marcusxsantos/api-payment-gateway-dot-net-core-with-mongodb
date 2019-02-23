using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clear.Sale.Mock.Model.Auth
{
	public class Login : ILogin
	{
		public string Apikey { get; set; }
		public string ClientID { get; set; }
		public string ClientSecret { get; set; }

		public bool IsValid()
		{
			if (Apikey != null && Apikey != string.Empty &&
				ClientID != null && ClientID != string.Empty &&
				ClientSecret != null && ClientSecret != string.Empty)
				return true;
			else
				return false;
		}
	}
}
