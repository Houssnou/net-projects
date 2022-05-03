using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace unit_testing_00
{
    public class BankAccount
    {
        private readonly ILogBook _logBook;
        public decimal Balance { get; set; }

        public BankAccount(ILogBook logBook)
        {
            _logBook = logBook;
            Balance = 0;
        }

        public bool Deposit(decimal amount)
        {
            Balance += amount;
            _logBook.Message("Deposit Success.");
            _logBook.Message("Test");
            _logBook.LogSeverity = 100;
            var getAtLeast = _logBook.LogSeverity;
            return true;
        }

        public bool Withdrawn(decimal amount)
        {
            if (amount > Balance)
            {
                return _logBook.LogBalanceAfterWithdrawal(Balance - amount);
            }

            _logBook.LogToDb("Withdrawal Amount: " + amount);
            Balance -= amount;
            return _logBook.LogBalanceAfterWithdrawal(Balance);
        }

        public decimal GetBalance()
        {
            return Balance;
        }

    }
}
