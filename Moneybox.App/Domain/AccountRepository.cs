using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.App.Domain
{
	public static class AccountRepository
    {
        public static void ValidateFromAccount(Account from, decimal amount, INotificationService notificationService)
        {
            var fromBalance = from.Balance - amount;
            if (fromBalance < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }

            if (fromBalance < 500m)
            {
                notificationService.NotifyFundsLow(from.User.Email);
            }            
        }
        
        public static void ValidateToAccount(Account to, decimal amount, INotificationService notificationService)
        {
            var paidIn = to.PaidIn + amount;
            if (paidIn > Account.PayInLimit)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            if (Account.PayInLimit - paidIn < 500m)
            {
                notificationService.NotifyApproachingPayInLimit(to.User.Email);
            }
        }

        public static void UpdateFromAccount(Account from, decimal amount, IAccountRepository accountRepository)
		{
            from.Balance -= amount;
            from.Withdrawn -= amount;
            accountRepository.Update(from);
        }

        public static void UpdateToAccount(Account to, decimal amount, IAccountRepository accountRepository)
        {
            to.Balance += amount;
            to.PaidIn += amount;
            accountRepository.Update(to);
        }
    }
}
