using Celsius.Commons;
using System;

namespace Celsius.Model
{
    public class Transaction
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public double Amount { get; set; }
        public string TransactionType { get; set; }
        public string SenderAccountNumber { get; set; }
        public string SenderAccountName { get; set; }
        public string ReceiverAccountName { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public string Description { get; set; }
        public double Balance { get; set; }
        public DateTime TransactionDate { get; set; }

        public Transaction()
        {
            Id = Guid.NewGuid().ToString();
            TransactionDate = DateTime.Now;
        }
    }
}