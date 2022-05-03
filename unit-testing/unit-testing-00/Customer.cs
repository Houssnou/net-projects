using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unit_testing_00
{
    public interface ICustomer
    {
        int Discount { get; set; }
        int OrderTotal { get; set; }
        string GreetMessage { get; set; }
        bool IsPlatinum { get; set; }
        string GetFullName(string firstName, string lastName);
    }

    public class Customer : ICustomer
    {
        public int Discount { get; set; }
        public int OrderTotal { get; set; }
        public string GreetMessage { get; set; }
        public bool IsPlatinum { get; set; }

        public Customer()
        {
            Discount = 15;
            IsPlatinum = false;
        }

        public string GetFullName(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName)) throw new ArgumentException("Empty First Name");

            GreetMessage = $"Hello, {firstName} {lastName}";
            return GreetMessage;
        }

        public CustomerType GetCustomerDetails()
        {
            return OrderTotal < 100 ? new BasicCustomer() : new PlatinumCustomer();
        }
    }

    public class CustomerType { }
    public class BasicCustomer : CustomerType { }
    public class PlatinumCustomer : CustomerType { }
}
