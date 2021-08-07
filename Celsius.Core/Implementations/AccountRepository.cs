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
    
    class AccountRepository : IAccountRepository
    {
        /// <summary>
        /// Creates Current account and returns a message to the user whether the process was successful or not
        /// </summary>
        /// <param name="model"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<string> CreateCurrentAccount(CurrentAccount model, Customer customer)
        {
            string message = "";
        
            string sp = "sp_CreateAccount";

            var parameters = DataUtils.SetAccountParameters(model, customer); 

            int result = await DataStore.WriteToDataTbl(sp, parameters);

            message += result == 1 ? "Account Created Successfully" : "Account Creation Failed";

            return message;
        }

        /// <summary>
        /// Creates Savings account and returns a message to the user whether the process was successful or not
        /// </summary>
        /// <param name="model"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<string> CreateSavingsAccount(SavingsAccount model, Customer customer)
        {
            string message = "";

            string sp = "sp_CreateAccount";

            var parameters = DataUtils.SetAccountParameters(model, customer);

            int result = await DataStore.WriteToDataTbl(sp, parameters);

            message += result == 1 ? "Account Created Successfully" : "Account Creation Failed";

            return message;
        }

        /// <summary>
        /// Returns the current account balance
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public async Task<string> GetAccountBalanceAsync(string accountNumber)
        {
            string sp = "sp_GetAccountsByAccountNumber";
            var account = await DataStore.ReadFromDataTbl(sp);
            string message = $"Your Account Balance is N{account}";

            return message;
        }

        /// <summary>
        /// Returns customer account details for the specified bank account.
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public async Task<Account> GetAccountDetailsAsync(string customerId)
        {
            string sp = "sp_GetAccountsByCustomerId";

            IDataParameter spParameters = new SqlParameter
            {
                ParameterName = "CustomerId",
                Value = customerId
            };

            Customer customer = new Customer();
            var account = customer.Account.Find(x => x.CustomerId == customerId);
            using (var reader = await DataStore.ReadFromDataTbl(sp, new IDataParameter[] { spParameters }))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        account.Id = (string)reader["Id"];
                        account.AccountNumber = (string)reader["AccountNumber"];
                        account.AccountType = (string)reader["AccountType"];
                        account.Balance = (double)reader["Balance"];
                        account.DateCreated = (DateTime)reader["DateCreated"];
                    }
                }

                return account;
            }
        }

        /// <summary>
        /// Get account Id using the account number
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public async Task<string> GetAccountIdAsync(string accountNumber)
        {
            string sp = "sp_GetAccountIdByAccountNumber";

            IDataParameter spParameters = new SqlParameter
            {
                ParameterName = "AccountNumber",
                Value = accountNumber
            };

            var accountId = await DataStore.ReadFromDataTbl(sp, new IDataParameter[] { spParameters });
            return accountId.ToString();

        }

        /// <summary>
        /// Returns a list of all the customers accounts and their current balance.
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public async Task<List<Account>> GetAccountList(string customerId)
        {
            string sp = "sp_GetAccountsByCustomerId";

            IDataParameter spParameters = new SqlParameter
            {
                ParameterName = "CustomerId",
                Value = customerId
            };

            List<Account> customerAccounts = new List<Account>();
            using (var reader = await DataStore.ReadFromDataTbl(sp, new IDataParameter[] { spParameters }))
            {
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        string accountType = (string)reader["AccountType"];

                        if (accountType.ToLower() == "savings")
                        {
                            SavingsAccount account = new SavingsAccount
                            {
                                AccountName = (string)reader["AccountName"],
                                Id = (string)reader["Id"],
                                CustomerId = (string)reader["CustomerId"],
                                AccountNumber = (string)reader["AccountNumber"],
                                AccountType = (string)reader["AccountType"],
                                Balance = (double)reader["Balance"],
                                DateCreated = (DateTime)reader["DateCreated"]
                            };
                            customerAccounts.Add(account);
                        }
                        else if(accountType.ToLower() == "current")
                        {
                            CurrentAccount account = new CurrentAccount
                            {
                                AccountName = (string)reader["AccountName"],
                                Id = (string)reader["Id"],
                                CustomerId = (string)reader["CustomerId"],
                                AccountNumber = (string)reader["AccountNumber"],
                                AccountType = (string)reader["AccountType"],
                                Balance = (double)reader["Balance"],
                                DateCreated = (DateTime)reader["DateCreated"]
                            };
                            customerAccounts.Add(account);
                        }
                    }
                }
                return customerAccounts;
            }
            
        }
    }
}
