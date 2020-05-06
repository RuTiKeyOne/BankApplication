using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public abstract class Account : IAccount
    {
        protected internal event AccountStateHandler Withdrawed;
        protected internal event AccountStateHandler Added;
        protected internal event AccountStateHandler Opened;
        protected internal event AccountStateHandler Closed;
        protected internal event AccountStateHandler Calculated;

        static int Counter = 0;
        protected int Days = 0;

        public Account(decimal sum, int percentage)
        {
            Sum = sum;
            Percentage = percentage;
            Id = ++Counter;
        }
        public decimal Sum { get; private set; }
        public int Percentage { get; private set; }
        public int Id { get; private set; }
        private void CallEvent(AccountEventArg e, AccountStateHandler handler)
        {
            if (e != null)
                handler?.Invoke(this, e);
        }
        protected virtual void OnOpened(AccountEventArg e)
        {
            CallEvent(e, Opened);
        }
        protected virtual void OnWithdrawed(AccountEventArg e)
        {
            CallEvent(e, Withdrawed);
        }
        protected virtual void OnAdded(AccountEventArg e)
        {
            CallEvent(e, Added);
        }
        protected virtual void OnClosed(AccountEventArg e)
        {
            CallEvent(e, Closed);
        }
        protected virtual void OnCalculated(AccountEventArg e)
        {
            CallEvent(e, Calculated);
        }

        public virtual void Put(decimal sum)
        {
            Sum += sum;
            OnAdded(new AccountEventArg("На счет поступило " + sum, sum));
        }
        public virtual decimal Withdraw(decimal sum)
        {
            decimal result = 0;
            if (Sum >= sum)
            {
                Sum -= sum;
                result = sum;
                OnWithdrawed(new AccountEventArg($"Сумма {sum} снята со счета {Id}", sum));
            }
            else
            {
                OnWithdrawed(new AccountEventArg($"Недостаточно денег на счете {Id}", 0));
            }
            return result;
        }
        protected internal virtual void Open()
        {
            OnOpened(new AccountEventArg($"Открыт новый счет! Id счета: {Id}", Sum));
        }
        protected internal virtual void Close()
        {
            OnClosed(new AccountEventArg($"Счет {Id} закрыт.  Итоговая сумма: {Sum}", Sum));
        }

        protected internal void IncrementDays()
        {
            Days++;
        }
        protected internal virtual void Calculate()
        {
            decimal increment = Sum * Percentage / 100;
            Sum = Sum + increment;
            OnCalculated(new AccountEventArg($"Начислены проценты в размере: {increment}", increment));
        }
    }
}
