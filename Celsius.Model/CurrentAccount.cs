﻿using Celsius.Commons;
using System;
using System.Collections.Generic;

namespace Celsius.Model
{
    public class CurrentAccount : Account
    {
        public new int MinimumBalance { get; private set; }

        private readonly int minimumBalance = 0;
        public CurrentAccount()
        {
            Id = Utils.Id.ToString();
            AccountNumber = Utils.GenerateAccountNumber();
            MinimumBalance = minimumBalance;
            TransactionHistory = new List<Transaction>();
            DateCreated = DateTime.Now;
            AccountType = "Current";
        }
    }
}