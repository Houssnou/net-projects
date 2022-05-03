using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unit_testing_00
{
    public interface ILogBook
    {
        public int LogSeverity { get; set; }
        public string LogType { get; set; }

        void Message(string message);
        bool LogToDb(string message);
        bool LogBalanceAfterWithdrawal(decimal amount);
        string LogWithStringReturn(string message);
        bool LogWithBooleanOutputResult(string message, out string output);
        bool LogWithRefObject(ref Customer customer);
    }

    public class LogBook : ILogBook
    {
        public int LogSeverity { get; set; }
        public string LogType { get; set; }

        public void Message(string message)
        {
            Console.WriteLine(message);
        }

        public bool LogToDb(string message)
        {
            Console.WriteLine(message);

            return true;
        }

        public bool LogBalanceAfterWithdrawal(decimal amount)
        {
            if (amount < 0)
            {
                Console.WriteLine("Failed.");
                return false;
            }
            
            Console.WriteLine("Success.");
            return true;
        }

        public string LogWithStringReturn(string message)
        {
            Console.WriteLine(message.ToLower());
            return message.ToLower();
        }

        public bool LogWithBooleanOutputResult(string message,  out string output)
        {
            output = "Hello, " + message;

            return true;
        }

        public bool LogWithRefObject(ref Customer customer)
        {
            return true;
        }
    }

    public class LogFaker : ILogBook
    {
        public int LogSeverity { get; set; }
        public string LogType { get; set; }

        public void Message(string message)
        {
        }

        public bool LogToDb(string message)
        {
            throw new NotImplementedException();
        }

        public bool LogBalanceAfterWithdrawal(decimal amount)
        {
            throw new NotImplementedException();
        }

        public string LogWithStringReturn(string message)
        {
            throw new NotImplementedException();
        }

        public bool LogWithBooleanOutputResult(string message, out string output)
        {
            throw new NotImplementedException();
        }

        public bool LogWithRefObject(ref Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
