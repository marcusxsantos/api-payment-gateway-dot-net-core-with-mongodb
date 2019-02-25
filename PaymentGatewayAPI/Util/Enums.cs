using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGatewayAPI.Util.Enums
{
	public enum eAntiFraudSatus {
		NotRequired =1,
		Valid = 2,
		Invalid = 3
	}

	public enum eAdquirente {
		Cielo = 1,
		Stone = 2
	}

	public enum eOperadoraCartao {
		Visa = 1,
		Master = 2
	}
}
