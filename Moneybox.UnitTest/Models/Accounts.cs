﻿using Moneybox.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.UnitTest.Models
{
	public static class Accounts
	{
		public static Account CreateNewAccount(User user, Guid guid)
		{
			return new Account()
			{
				Balance = 1000m,
				Id = guid,
				PaidIn = 0m,
				User = user,
				Withdrawn = 0m
			};
		}
	}
}
