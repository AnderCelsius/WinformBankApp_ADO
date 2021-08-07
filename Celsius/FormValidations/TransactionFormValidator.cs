using Celsius.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celsius.FormValidations
{
    class TransactionFormValidator : AbstractValidator<Transaction>
    {
        public TransactionFormValidator()
        {
            RuleFor(transaction => transaction.SenderAccountNumber).NotNull().Matches(@"\d{10}$");
            RuleFor(transaction => transaction.Description).NotNull().Matches(@"[A - Za - z]");
        }
    }
}
