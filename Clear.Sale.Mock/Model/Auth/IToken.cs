using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clear.Sale.Mock.Model.Auth
{
	public interface IToken
	{
		string Value { get; set; }
		DateTime ExpirationDate { get; set; }
	}
}
