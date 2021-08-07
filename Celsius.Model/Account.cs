using System;
using System.Collections.Generic;

namespace Celsius.Model
{
    public abstract class Account
    {
        public string Number;

        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public double Balance { get; set; }
        public abstract int MinimumBalance { get; protected set; }
        public DateTime DateCreated { get; set; }
        public List<Transaction> TransactionHistory { get; set; }

    }
}