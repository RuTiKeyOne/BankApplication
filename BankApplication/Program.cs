using BankLibrary;
using System;

namespace BankApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Bank<Account> Bank = new Bank<Account>("Сбербанк");
            bool Alive = true;
            while (Alive)
            {
                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("1. Открыть счет \t 2. Вывести средства  \t 3. Добавить на счет");
                Console.WriteLine("4. Закрыть счет \t 5. Пропустить день \t 6. Выйти из программы");
                Console.WriteLine("Введите номер пункта:");
                Console.ForegroundColor = color;
                try
                {
                    int Command = Convert.ToInt32(Console.ReadLine());
                    switch (Command)
                    {
                        case 1:
                            OpenAccount(Bank);
                            break;
                        case 2:
                            Withdraw(Bank);
                            break;
                        case 3:
                            Put(Bank);
                            break;
                        case 4:
                            break;
                        case 5:
                            break;
                        case 6:
                            Alive = false;
                            break;
                    }
                    Bank.CalculatePercentage();
                }
                catch(Exception ex)
                {
                    color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Неудачная попытка {ex.Message}");

                }              
            }
        }

        private static void OpenAccount(Bank<Account> Bank)
        {
            Console.WriteLine("Укажите сумму для создания счета:");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Выберите тип счета: 1. До востребования 2. Депозит");
            AccountType accountType;

            int type = Convert.ToInt32(Console.ReadLine());

            if (type == 2)
                accountType = AccountType.Deposit;
            else
                accountType = AccountType.Ordinary;

            Bank.Open(accountType,
                sum,
                AddSumHandler,
                WithdrawSumHandler, 
                (o, e) => Console.WriteLine(e.Message),
                CloseAccountHandler,
                OpenAccountHandler);
        }
        private static void Withdraw(Bank<Account> Bank)
        {
            Console.WriteLine("Укажите сумму для вывода со счета:");

            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Введите id счета:");
            int id = Convert.ToInt32(Console.ReadLine());

            Bank.Withdraw(sum, id);
        }
        private static void Put(Bank<Account> Bank)
        {
            Console.WriteLine("Укажите сумму, чтобы положить на счет:");
            decimal sum = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Введите Id счета:");
            int id = Convert.ToInt32(Console.ReadLine());
            Bank.Put(sum, id);
        }
        private static void CloseAccount(Bank<Account> Bank)
        {
            Console.WriteLine("Введите id счета, который надо закрыть:");
            int ID = Convert.ToInt32(Console.ReadLine());
            Bank.Close(ID);
        }
        private static void OpenAccountHandler(object o, AccountEventArg e)
        {
            Console.WriteLine(e.Message);
        }
        private static void AddSumHandler(object o, AccountEventArg e)
        {
            Console.WriteLine(e.Message);
        }
        private static void WithdrawSumHandler(object o, AccountEventArg e)
        {
            Console.WriteLine(e.Message);
            if(e.Sum > 0)
            {
                Console.WriteLine("У нас есть деньги");
            }
        }
        private static void CloseAccountHandler(object o, AccountEventArg e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
