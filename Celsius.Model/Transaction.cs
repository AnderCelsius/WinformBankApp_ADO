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
        public string Sender { get; set; }
        public string ReceiverAccountName { get; set; }
        public string ReceiverAccountNumber { get; set; }
        public string Description { get; set; }
        public double Balance { get; set; }
        public DateTime TransactionDate { get; set; }

        public Transaction()
        {
            Id = Utils.Id.ToString();
            TransactionDate = DateTime.Now;
        }
    }
}