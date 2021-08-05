using Celsius.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celsius.Model
{
    public class Customer
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get => LastName + " " + FirstName;
        }
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public DateTime DateCreated { get; set; }
        public List<Account> Account { get; set; }
        public Customer()
        {
            Id = Utils.Id.ToString();
            DateCreated = DateTime.Now;
            Account = new List<Account>();
        }
    }
}
