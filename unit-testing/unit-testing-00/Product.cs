using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace unit_testing_00
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public decimal GetPrice(ICustomer customer)
        {
            if (customer.IsPlatinum) return Price * (decimal).8;
            
            return Price;
        }

    }
}
