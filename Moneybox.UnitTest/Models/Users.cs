using Moneybox.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.UnitTest.Models
{
	public static class Users
	{
		public static User CreateUser()
		{
			return new User()
			{
				Email = "nh@hotmail.co.uk",
				Id = Guid.NewGuid(),
				Name = "Nathan"
			};
		}
	}
}
