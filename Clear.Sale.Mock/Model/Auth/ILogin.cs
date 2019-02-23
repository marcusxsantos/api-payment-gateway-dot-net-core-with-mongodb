using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clear.Sale.Mock.Model.Auth
{
	public interface ILogin
	{
		string Apikey { get; set; }
		string ClientID { get; set; }
		string ClientSecret { get; set; }

		bool IsValid();
	}
}
