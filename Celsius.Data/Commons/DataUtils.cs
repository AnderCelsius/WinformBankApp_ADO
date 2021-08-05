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
    }
}
