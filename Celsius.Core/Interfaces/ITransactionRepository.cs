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
        string MakeDeposit(Transaction model);
        string SendMoney(Transaction model);
        string TransferToOtherAccount(Transaction model, string otherAccountNumber);
        string MakeWithdrawal(Transaction model);
        string GetStatementOfAccount(string accountId);
        void RecordCreditTransaction(Transaction model, Transaction transaction, Account account);
        void RecordDebitTransaction(Transaction model, Transaction transaction, Account account);
    }
}
