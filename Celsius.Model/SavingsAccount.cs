using Celsius.Commons;
using System;
using System.Collections.Generic;

namespace Celsius.Model
{
    public class SavingsAccount : Account
    {
        public new int MinimumBalance { get; private set; }

        private readonly int minimumBalance = 1000;
        public SavingsAccount()
        {
            Id = Guid.NewGuid().ToString();
            AccountNumber = Utils.GenerateAccountNumber();
            MinimumBalance = minimumBalance;
            TransactionHistory = new List<Transaction>();
            DateCreated = DateTime.Now;
            AccountType = "Savings";
        }
    }
}