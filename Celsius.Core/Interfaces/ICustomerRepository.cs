using Celsius.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celsius.Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<string> RegisterCustomer(Customer model);
        Task<bool> SearchCustomer(string email);
        Task<int> AddCustomerInfoToDB(Customer model);
        Task<Customer> GetCustomerDetails(string email);
    }
}
