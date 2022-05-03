//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace unit_testing_00
//{
    
//    public class Customer_Old
//    {
//        public int Discount = 15;
//        public int OrderTotal { get; set; }
//        public string GreetMessage { get; set; }
//        public bool IsPlatinum { get; set; }

//        public Customer_Old()
//        {
//            IsPlatinum = false;
//        }

//        public string GetFullName(string firstName, string lastName)
//        {
//            if (string.IsNullOrEmpty(firstName)) throw new ArgumentException("Empty First Name");

//            GreetMessage = $"Hello, {firstName} {lastName}";
//            return GreetMessage;
//        }

//        public CustomerType GetCustomerDetails()
//        {
//            return OrderTotal < 100 ? new BasicCustomer() : new PlatinumCustomer();
//        }
//    }

//    public class CustomerType { }
//    public class BasicCustomer : CustomerType { }
//    public class PlatinumCustomer : CustomerType { }
//}
