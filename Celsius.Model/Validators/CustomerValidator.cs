using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celsius.Model.Validators
{
    class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.FirstName).NotNull().Matches("[A-Za-z]");
            RuleFor(customer => customer.LastName).NotNull().Matches("[A-Za-z]");
            RuleFor(customer => customer.Email).NotNull().EmailAddress();
            RuleFor(customer => customer.PhoneNumber).NotNull().Matches(@"^[0]\d{10}$");
            //RuleFor(customer => customer.PasswordHash).NotNull().Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,15}$");
        }
    }
}
