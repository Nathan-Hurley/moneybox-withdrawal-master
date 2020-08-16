using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moneybox.App;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using Moneybox.App.Features;
using Moneybox.UnitTest.Models;
using System;

namespace Moneybox.UnitTest
{
	[TestClass]
	public class MoneyTest
	{
		IAccountRepository accountRepository;
		INotificationService notificationService;

		User user;
		Account account1, account2;

		public MoneyTest()
		{
			user = Users.CreateNewUser();
			account1 = Accounts.CreateNewAccount(user);
			account2 = Accounts.CreateNewAccount(user);
		}

		[TestMethod]
		public void Transfer_100_From_Acount1_To_Account2()
		{
			var transferMoney = new TransferMoney(accountRepository, notificationService);
			var fromAccount = account1.Id;
			var toAccount = account2.Id;

			transferMoney.Execute(fromAccount, toAccount, 100.0m);

			Assert.IsTrue(account1.Balance > account2.Balance);
		}

		[TestMethod]
		public void Withdraw_100_From_Account1()
		{
			var withdrawMoney = new WithdrawMoney(accountRepository, notificationService);
			var fromAccount = account1.Id;

			withdrawMoney.Execute(fromAccount, 100.0m);

		}
	}
}
