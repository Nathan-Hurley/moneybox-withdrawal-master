using Moneybox.App.DataAccess;
using Moneybox.App.Domain;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;

        public TransferMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var from = accountRepository.GetAccountById(fromAccountId);
            var to = accountRepository.GetAccountById(toAccountId);

            AccountRepository.ValidateFromAccount(from, amount, notificationService);
            AccountRepository.ValidateToAccount(to, amount, notificationService);

            AccountRepository.UpdateFromAccount(from, amount, accountRepository);
            AccountRepository.UpdateToAccount(to, amount, accountRepository);
        }
    }
}
