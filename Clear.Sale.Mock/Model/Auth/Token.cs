using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clear.Sale.Mock.Model.Auth
{
	public class Token : IToken
	{
		public string Value { get; set; }
		public DateTime ExpirationDate { get; set; }
	}
}
