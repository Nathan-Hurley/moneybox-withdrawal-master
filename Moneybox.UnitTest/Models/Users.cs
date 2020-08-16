using Moneybox.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.UnitTest.Models
{
	public static class Users
	{
		public static User CreateNewUser()
		{
			return new User()
			{
				Email = "nathan.hurley@hotmail.co.uk",
				Id = Guid.NewGuid(),
				Name = "Nathan"
			};
		}
	}
}
