using System;
using System.Collections.Generic;
using System.Text;

namespace BankLibrary
{
    public enum AccountType
    {
        Ordinary,
        Deposit
    }
    public class Bank<T> where T : Account
    {
        T[] Accounts;
        public string Name { get; private set; }
        public Bank(string name)
        {
            this.Name = name;
        }
        public void Open(AccountType accounttype, decimal sum, AccountStateHandler addSumHandler, AccountStateHandler withdrawSumHandler, AccountStateHandler calculateHandler, AccountStateHandler closeAccountHandler, AccountStateHandler openAccountHandler)
        {
            T newAccount = null;
            switch (accounttype)
            {
                case AccountType.Ordinary :
                newAccount = new DemandAccount(sum, 1) as T;
                break;
                case AccountType.Deposit:
                newAccount = new DemandAccount(sum, 40) as T;
                break;
            }
            if (newAccount == null) throw new Exception("Ошибка создания счета");
            if(Accounts == null)
            {
                Accounts = new T[] { newAccount };
            }
            else
            {
                T[] TempAccounts = new T [Accounts.Length + 1];
                for(int x = 0; x<Accounts.Length; x++) TempAccounts[x] = Accounts[x];
                TempAccounts[Accounts.Length - 1] = newAccount;
                Accounts = TempAccounts;
            }
            newAccount.Added += addSumHandler;
            newAccount.Withdrawed += withdrawSumHandler;
            newAccount.Calculated += calculateHandler;
            newAccount.Closed += closeAccountHandler;
            newAccount.Opened += openAccountHandler;
            newAccount.Open();
        }
        public void Put(decimal sum, int id)
        {
            T Account = FintAccout(id);
            if (Account == null) throw new Exception("Счет не найден");
            Account.Put(sum);

        }
        public void Withdraw(decimal sum, int id)
        {
            T Account = FintAccout(id);
            if (Account == null) throw new Exception("Счет не найден");
            Account.Withdraw(sum);
        }
        public void Close(int id)
        {
            int index;
            T Account = FintAccout(id, out index);
            if (Account == null) throw new Exception("Счет не найден");
            Account.Close();
            if(Accounts.Length <= 1)
            {
                Accounts = null;
            } else
            {
                T[] TempAccounts = new T[Accounts.Length - 1];
                for(int x = 0, j=0; x < Accounts.Length; x++)
                {
                    TempAccounts[j++] = Accounts[x];
                }
                Accounts = TempAccounts;
            }
        }
        public void CalculatePercentage()
        {
            if (Accounts == null) return;
            for(int x = 0; x<Accounts.Length; x++)
            {
                Accounts[x].IncrementDays();
                Accounts[x].Calculate();
            }
        }
        public T FintAccout(int id)
        {
            for(int x = 0; x<Accounts.Length; x++)
            {
                if (Accounts[x].Id == id) return Accounts[x];
            }
            return null;
        }
        public T FintAccout(int id, out int index)
        {
            for (int x = 0; x < Accounts.Length; x++)
            {
                index = x;
                if (Accounts[x].Id == id) return Accounts[x];
            }
            index = -1;
            return null;
        }
    }
}
