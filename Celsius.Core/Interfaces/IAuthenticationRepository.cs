using Celsius.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celsius.Core.Interfaces
{
    public interface IAuthenticationRepository
    {
        bool Login(string password, Customer customer);
        bool Logout();
    }
}
