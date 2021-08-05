using Celsius.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celsius.Core.Interfaces
{
    public interface IAccountRepository
    {
        Task<string> CreateCurrentAccount(CurrentAccount model, Customer customer);
        Task<string> CreateSavingsAccount(SavingsAccount model, Customer customer);
        Task<Account> GetAccountDetailsAsync(string accountNumber);
        Task<string> GetAccountBalanceAsync(string accountNumber);
         Task<List<Account>> GetAccountList(string customerID);
    }
}
