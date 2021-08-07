using Celsius.Commons;
using Celsius.Core.Interfaces;
using Celsius.Data;
using Celsius.Data.Commons;
using Celsius.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celsius.Core.Implementations
{
    public class TransactionRepository : ITransactionRepository
    {
        public async Task<List<Transaction>> GetListOfTransactionsAsync(string accountId)
        {
            string sp = "sp_GetAllTransactionsByAccountNumber";

            IDataParameter spParameters = new SqlParameter
            {
                ParameterName = "AccountId",
                Value = accountId
            };

            List<Transaction> transactions = new List<Transaction>();

            using (var reader = await DataStore.ReadFromDataTbl(sp, new IDataParameter[] { spParameters }))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        Transaction transaction = new Transaction
                        {
                            TransactionDate = (DateTime)reader["TransactionDate"],
                            TransactionType = (string)reader["TransactionType"],
                            Amount = (double)reader["Amount"],
                            Description = (string)reader["Description"]
                        };
                        transactions.Add(transaction);
                    }
                }

                return transactions;
            }
        }


        public async Task<List<Transaction>> GetTop5ListOfTransactionsAsync(string accountId)
        {
            string sp = "sp_GetTop5TransactionsByAccountNumber";

            IDataParameter spParameters = new SqlParameter
            {
                ParameterName = "AccountId",
                Value = accountId
            };

            List<Transaction> transactions = new List<Transaction>();

            using (var reader = await DataStore.ReadFromDataTbl(sp, new IDataParameter[] { spParameters }))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        Transaction transaction = new Transaction
                        {
                            TransactionDate = (DateTime)reader["TransactionDate"],
                            TransactionType = (string)reader["TransactionType"],
                            Amount = (double)reader["Amount"],
                            Description = (string)reader["Description"]
                        };
                        transactions.Add(transaction);
                    }
                }

                return transactions;
            }
        }

        public void RecordCreditTransaction(Transaction model, Transaction transaction, Account account)
        {
            transaction.AccountId = account.Id;
            transaction.Amount = model.Amount;
            transaction.Description = model.Description;
            transaction.SenderAccountName = account.AccountName;
            transaction.TransactionType = Utils.TransactionType.Cr.ToString();
            account.Balance += model.Amount;
            transaction.Balance = account.Balance;
            account.TransactionHistory.Add(transaction);
        }

        public void RecordDebitTransaction(Transaction model, Transaction transaction, Account account)
        {
            transaction.AccountId = model.AccountId;
            transaction.Amount = model.Amount;
            transaction.SenderAccountName = account.AccountName;
            transaction.ReceiverAccountNumber = model.ReceiverAccountNumber;
            transaction.Description = model.Description;
            transaction.TransactionType = Utils.TransactionType.Dr.ToString();
            account.Balance -= model.Amount;
            transaction.Balance = account.Balance;
            account.TransactionHistory.Add(transaction);
        }

        public async Task<string> MakeDepositAsync(Transaction model, Account account)
        {
            string message = "";

            string sp = "sp_DepositByAccountId";

            //(@Id, @AccountId, @Amount, @SenderAccountName, @SenderAccountNumber, @TransactionType, @Description, @Balance, @TransactionDate)

            IDataParameter pId, pAccountId, pAmount, pBalance, pSenderAccountNumber, pSenderAccountName, pTransactionType, pDescription, pTransactionDate; // instance of sqlparamet

            pId = new SqlParameter("@Id", model.Id);

            pAccountId = new SqlParameter("@AccountId", account.Id);

            //pAccountNumber = new SqlParameter("@AccountNumber", account.AccountNumber);

            //pAccountName = new SqlParameter("@AccountName", account.AccountName);

            pSenderAccountNumber = new SqlParameter("@SenderAccountNumber", model.SenderAccountNumber);

            pSenderAccountName = new SqlParameter("@SenderAccountName", model.SenderAccountName);

            pAmount = new SqlParameter("@Amount", model.Amount);

            pBalance = new SqlParameter("@Balance", model.Balance);

            pTransactionType = new SqlParameter("@TransactionType", model.TransactionType);

            pDescription = new SqlParameter("@Description", model.Description);

            pTransactionDate = new SqlParameter("@TransactionDate", model.TransactionDate);

            int result = await DataStore.WriteToDataTbl(sp, new IDataParameter[] { pId, pAccountId, pAmount, pBalance, pSenderAccountNumber, pSenderAccountName, pTransactionType, pDescription, pTransactionDate });

            message += result == 1 ? "Transaction Successful" : "Transaction Failed";

            return message;
        }

        public async Task<string> MakeWithdrawalAsync(Transaction model, Account account)
        {
            string message = "";

            string sp = "sp_WithdrawByAccountNumber";

            IDataParameter pId, pAccountNumber, pAccountId, pAmount, pBalance, pTransactionType, pDescription, pTransactionDate; // instance of sqlparamet

            pId = new SqlParameter("@Id", model.Id);

            pAccountId = new SqlParameter("@AccountId", account.Id);

            pAccountNumber = new SqlParameter("@AccountNumber", account.AccountNumber);

            pAmount = new SqlParameter("@Amount", model.Amount);

            pBalance = new SqlParameter("@Balance", model.Balance);

            pTransactionType = new SqlParameter("@TransactionType", model.TransactionType);

            pDescription = new SqlParameter("@Description", model.Description);

            pTransactionDate = new SqlParameter("@TransactionDate", model.TransactionDate);

            int result = await DataStore.WriteToDataTbl(sp, new IDataParameter[] { pId, pAccountId, pAccountNumber, pAmount, pBalance, pTransactionType, pDescription, pTransactionDate });

            message += result == 1 ? "Transaction Successful" : "Transaction Failed";

            return message;
        }

        public async Task<string> SendMoneyAsync(Transaction model, Account account)
        {
            string message = "";

            string sp = "sp_SendToOtherByAccountNumber";

            IDataParameter pId, pAccountNumber, pAccountId, pAmount, pReceiverAccNum, /*pReceiverAccName,*/ pTransactionType, pDescription, pBalance, pTransactionDate; // instance of sqlparamet

            pId = new SqlParameter("@Id", model.Id);

            pAccountId = new SqlParameter("@AccountId", account.Id);

            pAccountNumber = new SqlParameter("@AccountNumber", account.AccountNumber);

            pReceiverAccNum = new SqlParameter("@ReceiverAccountNumber", model.ReceiverAccountNumber);

            //pReceiverAccName = new SqlParameter("@ReceiverAccountName", model.ReceiverAccountName);

            pAmount = new SqlParameter("@Amount", model.Amount);

            pTransactionType = new SqlParameter("@TransactionType", model.TransactionType);

            pDescription = new SqlParameter("@Description", model.Description);

            pBalance = new SqlParameter("@Balance", account.Balance);

            pTransactionDate = new SqlParameter("@TransactionDate", model.TransactionDate);

            int result = await DataStore.WriteToDataTbl(sp, new IDataParameter[] { pId, pAccountId, pAccountNumber, pReceiverAccNum, /*pReceiverAccName,*/ pAmount, pTransactionType, pDescription, pBalance, pTransactionDate });

            message += result == 1 ? "Transaction Successful" : "Transaction Failed";

            return message;
        }

        public async Task<string> TransferToOtherAccountAsync(Transaction model, Account account)
        {
            string message = "";

            string sp = "sp_SendToOtherByAccountNumber";

            IDataParameter pId, pAccountNumber, pAccountId, pAmount, pReceiverAccNum, pTransactionType, pDescription, pBalance, pTransactionDate; // instance of sqlparamet

            pId = new SqlParameter("@Id", model.Id);

            pAccountId = new SqlParameter("@AccountId", account.Id);

            pAccountNumber = new SqlParameter("@AccountNumber", account.AccountNumber);

            pReceiverAccNum = new SqlParameter("@ReceiverAccountNumber", model.ReceiverAccountNumber);

            pAmount = new SqlParameter("@Amount", model.Amount);

            pTransactionType = new SqlParameter("@TransactionType", model.TransactionType);

            pDescription = new SqlParameter("@Description", model.Description);

            pBalance = new SqlParameter("@Balance", account.Balance);

            pTransactionDate = new SqlParameter("@TransactionDate", model.TransactionDate);

            int result = await DataStore.WriteToDataTbl(sp, new IDataParameter[] { pId, pAccountId, pAccountNumber, pReceiverAccNum, pAmount, pTransactionType, pDescription, pBalance, pTransactionDate });

            message += result == 1 ? "Transaction Successful" : "Transaction Failed";

            return message;
        }
    }
}
