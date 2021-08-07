using Celsius.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celsius.Core.Interfaces
{
    public interface ITransactionRepository
    {
        Task<string> MakeDepositAsync(Transaction model, Account account);
        Task<string> SendMoneyAsync(Transaction model, Account account);
        Task<string> TransferToOtherAccountAsync(Transaction model, Account account);
        Task<string> MakeWithdrawalAsync(Transaction model, Account account);
        Task<List<Transaction>> GetListOfTransactionsAsync(string accountId);
        Task<List<Transaction>> GetTop5ListOfTransactionsAsync(string accountId);
        void RecordCreditTransaction(Transaction model, Transaction transaction, Account account);
        void RecordDebitTransaction(Transaction model, Transaction transaction, Account account);
    }
}
