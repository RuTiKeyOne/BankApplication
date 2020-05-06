using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public delegate void AccountStateHandler(object sender, AccountEventArg a);
    public class AccountEventArg
    {
        public string Message { get; private set; }
        public decimal Sum { get; private set; }
        public AccountEventArg(string message, decimal sum)
        {
            Message = message;
            Sum = sum;
        }

    }
}
