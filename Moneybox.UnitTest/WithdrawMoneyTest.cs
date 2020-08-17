using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moneybox.App;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using Moneybox.App.Features;
using Moneybox.UnitTest.Models;
using Moq;
using System;
using System.Net;
using Xunit.Sdk;

namespace Moneybox.UnitTest
{
	[TestClass]
	public class WithdrawMoneyTest
	{
		readonly User user;
		readonly Account account;

		public WithdrawMoneyTest()
		{
			user = Users.CreateUser();
			account = Accounts.CreateNewAccount(user, new Guid("5fb7097c-335c-4d07-b4fd-000004e2d28c"));
		}

		public Mock<IAccountRepository> CreateAccountRepository(Account fromAccount)
		{
			var accountRepository = new Mock<IAccountRepository>();
			accountRepository.Setup(x => x.GetAccountById(fromAccount.Id)).Returns(fromAccount);
			accountRepository.Setup(x => x.Update(fromAccount)).Callback((Account account) => fromAccount.Withdrawn = 100).Verifiable();
			return accountRepository;
		}

		public Mock<INotificationService> CreateNotificationService()
		{
			var notificationServiceRepository = new Mock<INotificationService>();
			notificationServiceRepository.Setup(x => x.NotifyApproachingPayInLimit("nh@hotmail.co.uk"));
			notificationServiceRepository.Setup(x => x.NotifyFundsLow("nh@hotmail.co.uk"));
			return notificationServiceRepository;
		}

		[TestMethod]
		public void Withdraw_100_From_Acount()
		{
			// Arrange
			var accountRepository = CreateAccountRepository(account);
			var notificationServiceRepository = CreateNotificationService();

			// Act
			var withdrawMoney = new WithdrawMoney(accountRepository.Object, notificationServiceRepository.Object);
			withdrawMoney.Execute(account.Id, 100m);

			// Assert
			accountRepository.VerifyAll();
		}
	}
}
