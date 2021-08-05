using Celsius.Commons;
using Celsius.Core.Interfaces;
using Celsius.Data;
using Celsius.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celsius.Core.Implementations
{
    public class TransactionRepository : ITransactionRepository
    {
        private static readonly string transactionFilePath = @"Transaction.json";
        private static readonly string accountFilePath = @"Account.json";
        public string GetStatementOfAccount(string accountId)
        {
            var message = string.Empty;
            //List<Transaction> transactionList = DataStore<Transaction>.ReadFromDataTbl(transactionFilePath);
            //List<Account> accountList = DataStore<Account>.ReadFromDataTbl(accountFilePath);

            //if (transactionList.Count != 0)
            //{
            //    var transactions = accountList.FirstOrDefault(account => account.Id == accountId).TransactionHistory;

            //    foreach (var transaction in transactions)
            //    {
            //        //$"{transaction.TransactionDate}", $"{transaction.Description}", $"{transaction.Amount}{transaction.TransactionType}", $"N{transaction.Balance}");
            //    }
            //}
            //else
            //{
            //    message += $"No transaction made yet";
            //}

            return message;
        }

        public void RecordCreditTransaction(Transaction model, Transaction transaction, Account account)
        {
            transaction.AccountId = account.Id;
            transaction.Amount = model.Amount;
            transaction.Description = model.Description;
            transaction.Sender = account.AccountName;
            transaction.TransactionType = Utils.TransactionType.Cr.ToString();
            account.Balance += model.Amount;
            transaction.Balance = account.Balance;
            account.TransactionHistory.Add(transaction);
        }

        public void RecordDebitTransaction(Transaction model, Transaction transaction, Account account)
        {
            transaction.AccountId = model.AccountId;
            transaction.Amount = model.Amount;
            transaction.Sender = account.AccountName;
            transaction.ReceiverAccountNumber = model.ReceiverAccountNumber;
            transaction.Description = model.Description;
            transaction.TransactionType = Utils.TransactionType.Dr.ToString();
            account.Balance -= model.Amount;
            transaction.Balance = account.Balance;
            account.TransactionHistory.Add(transaction);
        }

        public string MakeDeposit(Transaction model)
        {
            var message = string.Empty;
            //List<Transaction> transactionList = DataStore<Transaction>.ReadFromDataTbl(transactionFilePath);
            //List<Account> accountList = DataStore<Account>.ReadFromDataTbl(accountFilePath);
            //var previousTransactionCount = transactionList.Count;
            //var transaction = new Transaction();

            //foreach (var account in accountList)
            //{
            //    if(model != null)
            //    {
            //        if (account.Id == model.AccountId && model.Amount > 0)
            //        {
            //            RecordCreditTransaction(model, transaction, account);
            //        }
            //    }
            //}
            //transactionList.Add(transaction);

            //int updatedTransactionCount = transactionList.Count;

            //if (updatedTransactionCount > previousTransactionCount)
            //    message += "Transaction Succesful";

            //else
            //{
            //    message += "Transaction Failed";
            //}

            return message;
        }

        public string MakeWithdrawal(Transaction model)
        {
            var message = string.Empty;
            //List<Transaction> transactionList = DataStore<Transaction>.ReadFromDataTbl(transactionFilePath);
            //List<Account> accountList = DataStore<Account>.ReadFromDataTbl(accountFilePath);
            //int previousTransactionCount = transactionList.Count;
            //var transaction = new Transaction();

            //foreach (var account in accountList)
            //{
            //    if(model != null)
            //    {
            //        if (account.Id == model.AccountId)
            //        {
            //            if (account.Balance >= model.Amount + account.MinimumBalance)
            //            {
            //                RecordDebitTransaction(model, transaction, account);
            //            }
            //            else
            //            {
            //                return "Insufficient Funds.";
            //            }
            //        }
            //    }
            //}
            //transactionList.Add(transaction);
            //int updatedTransactionCount = transactionList.Count;

            //if (updatedTransactionCount > previousTransactionCount)
            //{
            //    message += "Transaction Succesful";
            //}
            //else
            //{
            //    message += "Transaction Failed";
            //}

            return message;
        }

        public string SendMoney(Transaction model)
        {
            var message = string.Empty;
            //List<Transaction> transactionList = DataStore<Transaction>.ReadFromDataTbl(transactionFilePath);
            //List<Account> accountList = DataStore<Account>.ReadFromDataTbl(accountFilePath);

            //int previousTransactionCount = transactionList.Count;
            //var transaction = new Transaction();

            //foreach (var account in accountList)
            //{
            //    if (account.Id == model.AccountId)
            //    {
            //        if (account.Balance >= model.Amount - account.MinimumBalance)
            //        {
            //            RecordDebitTransaction(model, transaction, account);
            //        }
            //        else
            //        {
            //            return "Insufficient Funds.";
            //        }
            //    }
            //}

            //transactionList.Add(transaction);
            //int updatedTransactionCount = transactionList.Count;

            //if (updatedTransactionCount > previousTransactionCount)
            //{
            //    message += "Transaction Succesful";
            //}
            //else
            //{
            //    message += "Transaction Failed";
            //}
            return message;
        }

        public string TransferToOtherAccount(Transaction model, string otherAccountNumber)
        {
            var message = string.Empty;
            //List<Transaction> transactionList = DataStore<Transaction>.ReadFromDataTbl(transactionFilePath);
            //List<Account> accountList = DataStore<Account>.ReadFromDataTbl(accountFilePath);
            //Account foundAccount = (Account)DataStore<Account>.ReadFromDataTbl(accountFilePath).Where(x => x.Number == otherAccountNumber);

            //int previousTransactionCount = transactionList.Count;
            //var transaction = new Transaction();
            //var receiverTransaction = new Transaction();

            ////Debit sending account
            //foreach (var account in accountList)
            //{
            //    if (account.Id == model.AccountId)
            //    {
            //        if (account.Balance >= model.Amount - account.MinimumBalance)
            //        {
            //            RecordDebitTransaction(model, transaction, account);
            //        }
            //        else
            //        {
            //            return "Insufficient Funds.";
            //        }
            //    }
            //}

            ////Credit recieving account
            //foreach (var account in accountList)
            //{
            //    if (account.Number == otherAccountNumber)
            //    {
            //        receiverTransaction.AccountId = foundAccount.Number;
            //        RecordCreditTransaction(model, receiverTransaction, account);
            //    }
            //}

            ////Add record to transferring account
            //transactionList.Add(transaction);
            ////Add record to receiving account
            //transactionList.Add(receiverTransaction);

            //int updatedTransactionCount = transactionList.Count;

            //if (updatedTransactionCount > previousTransactionCount)
            //    message += "Transaction Succesful";

            //else
            //{
            //    message += "Transaction Failed";
            //}
            return message;
        }
    }
}
