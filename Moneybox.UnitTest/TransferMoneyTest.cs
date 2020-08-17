using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moneybox.App;
using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using Moneybox.App.Features;
using Moneybox.UnitTest.Models;
using Moq;
using System;
using Xunit.Sdk;

namespace Moneybox.UnitTest
{
	[TestClass]
	public class TransferMoneyTest
	{
		readonly User user;
		readonly Account fromAccount, toAccount;

		public TransferMoneyTest()
		{
			user = Users.CreateUser();
			fromAccount = Accounts.CreateNewAccount(user, new Guid("5fb7097c-335c-4d07-b4fd-000004e2d28c"));
			toAccount = Accounts.CreateNewAccount(user, new Guid("3a40c056-b73d-469b-bddd-df55ab4e91a3"));
		}

		public Mock<IAccountRepository> CreateAccountRepository(Account fromAccount, Account toAccount)
		{
			var accountRepository = new Mock<IAccountRepository>();
			accountRepository.Setup(x => x.GetAccountById(fromAccount.Id)).Returns(fromAccount);
			accountRepository.Setup(x => x.GetAccountById(toAccount.Id)).Returns(toAccount);
			accountRepository.Setup(x => x.Update(fromAccount)).Callback((Account account) => fromAccount.Balance = 900);
			accountRepository.Setup(x => x.Update(toAccount)).Verifiable();
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
		public void Transfer_100_From_Acount1_To_Account2()
		{
			// Arrange
			var accountRepository = CreateAccountRepository(fromAccount, toAccount);
			var notificationServiceRepository = CreateNotificationService();

			// Act
			var transferMoney = new TransferMoney(accountRepository.Object, notificationServiceRepository.Object);
			transferMoney.Execute(fromAccount.Id, toAccount.Id, 100m);

			// Assert
			accountRepository.VerifyAll();
		}
	}
}
