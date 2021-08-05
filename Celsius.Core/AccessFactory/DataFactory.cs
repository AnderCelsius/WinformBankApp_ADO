using Celsius.Core.Implementations;
using Celsius.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celsius.Core.AccessFactory
{
    public class DataFactory
    {
        public static IAuthenticationRepository GetAuthenticationRepository()
        {
            return new AuthenticationRepository();
        }

        public static ICustomerRepository GetCustomerRepository()
        {
            return new CustomerRepository();
        }

        public static IAccountRepository GetAccountRepository()
        {
            return new AccountRepository();
        }
    }
}
