using Celsius.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celsius.Data.Commons
{
    public class DataUtils
    {
        public static IDataParameter[] SetAccountParameters(Account model, Customer customer)
        {
            IDataParameter pId, pCustomerId, pAccountName, pAccountNumber, pAccountType, pBalance, pMinimumBalance, pDateCreated; // instance of sqlparameter;

            if (model == null)
            {
                return null;
            }

            pId = new SqlParameter("@Id", model.Id);

            pCustomerId = new SqlParameter("@CustomerId", customer.Id);

            pAccountName = new SqlParameter("@AccountName", customer.FullName);


            pAccountNumber = new SqlParameter("@AccountNumber", model.AccountNumber);


            pAccountType = new SqlParameter("@AccountType", model.AccountType);

            pBalance = new SqlParameter("@Balance", model.Balance);

            pMinimumBalance = new SqlParameter("@MinimumBalance", model.MinimumBalance);

            pDateCreated = new SqlParameter("@DateCreated", model.DateCreated);

            return new IDataParameter[] { pId, pCustomerId, pAccountName, pAccountNumber, pAccountType, pBalance, pMinimumBalance, pDateCreated };
        }


        public static IDataParameter[] SetTransactionParameters(Transaction model, Account account)
        {
            IDataParameter pId, pAccountId, pAmount, pTransactionType, pSenderAccountName, pReceiverAccountName, pReceiverAccountNumber, pDescription, pBalance, pTransactionDate; // instance of sqlparameter;

            if (model == null)
            {
                return null;
            }

            pId = new SqlParameter("@Id", model.Id);

            pAccountId = new SqlParameter("@CustomerId", account.Id);

            pAmount = new SqlParameter("@AccountName", model.Amount);


            pTransactionType = new SqlParameter("@AccountNumber", model.TransactionType);


            pSenderAccountName = new SqlParameter("@AccountType", model.SenderAccountName);

            pBalance = new SqlParameter("@Balance", model.Balance);

            pReceiverAccountName = new SqlParameter("@MinimumBalance", model.ReceiverAccountName);

            pReceiverAccountNumber = new SqlParameter("@DateCreated", model.ReceiverAccountNumber);

            pDescription = new SqlParameter("@DateCreated", model.Description);

            pTransactionDate = new SqlParameter("@DateCreated", model.TransactionDate);

            return new IDataParameter[] { pId, pAccountId, pAmount, pTransactionType, pSenderAccountName, pBalance, pReceiverAccountName, pReceiverAccountNumber, pDescription, pTransactionDate };
        }
    }
}
