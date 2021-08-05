using Celsius.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Celsius.Model;
using Celsius.Commons;

namespace Celsius.Core.Implementations
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        public bool Login(string password, Customer customer)
        {
            bool isValid = Utils.CompareHash(password, customer.PasswordSalt, customer.PasswordHash);
            return isValid;
        }

        public bool Logout()
        {
            return true;
        }
    }
}
