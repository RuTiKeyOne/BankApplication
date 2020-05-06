using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    class DepositAccount : Account
    {
        public DepositAccount(decimal sum, int percentage) : base(sum, percentage)
        {

        }
        protected internal override void Open()
        {
            base.OnOpened(new AccountEventArg($"Открыт новый счет с номером {this.Id}", this.Sum));
        }
        protected internal void Put(decimal sum)
        {
            if(Days % 30 == 0)
            {
                base.Put(sum);
            }
            else
            {
                base.OnAdded(new AccountEventArg("На счет можно положить только после 30-ти дневного периода", 0));
            }
        }
        protected internal decimal Withdraw(decimal sum)
        {
            if (Days % 30 == 0)
            {
                return base.Withdraw(sum);
            }
            else
            {
                base.OnWithdrawed(new AccountEventArg("Вывести средства можно только после 30-ти дневного периода", 0));
                return 0;
            }
        }
        protected internal void Calculate()
        {
            if (Days % 30 == 0)
            {
                base.Calculate();
            }
        }
    }
}
