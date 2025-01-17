﻿using Moneybox.App.DataAccess;
using Moneybox.App.Domain;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;

        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            var from = accountRepository.GetAccountById(fromAccountId);

            AccountRepository.ValidateFromAccount(from, amount, notificationService);
            AccountRepository.UpdateFromAccount(from, amount, accountRepository);
        }
    }
}
