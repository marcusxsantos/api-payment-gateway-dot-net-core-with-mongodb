﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clear.Sale.Mock.Model.Auth
{
	public class RequestAuth : IRequestAuth
	{
		public Login Login { get; set; }
	}
}
