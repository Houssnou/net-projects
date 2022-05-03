using System;
using unit_testing_00;
using Xunit;

namespace unit_Fact_00_XUnit
{
    public class CustomerXUnitFacts
    {
        private Customer customer;

        public CustomerXUnitFacts()
        {
            customer = new();
        }
        [Fact]
        public void ShouldReturnCustomerFullName()
        {
            //using SetUp instead of repeating Class initialization in each Fact
            ////Arrange
            //_customer _customer = new(); 

            //Act
            string fullName = customer.GetFullName("John", "Doe");

            //Assert
            Assert.Equal("Hello, John Doe", fullName);
            Assert.Contains("John", fullName);
            Assert.StartsWith("Hello", fullName);
            Assert.EndsWith("Doe", fullName);

            Assert.Equal("Hello, John Doe", fullName);

            Assert.Matches("Hello, [A-Z]{1}[a-z]+ ", fullName);
        }

        //[Fact]
        //public void ShouldReturnCustomerFullNameWithMultipleAssert()
        //{
        //    //Act
        //    string fullName = _customer.GetFullName("John", "Doe");

        //    //Assert 
        //    //Multiple asserts only available in 2.4.2 preview
        //    Assert.Multiple(
        //        () => Assert.Equal("Hello, John Doe", fullName),
        //        () => Assert.Contains("John", fullName),
        //        () => Assert.StartsWith("Hello", fullName),
        //        () => Assert.EndsWith("Doe", fullName),

        //        () => Assert.Equal("Hello, John Doe", fullName),

        //        () => Assert.Matches("Hello, [A-Z]{1}[a-z]+ ", fullName)
        //    );
        //}

        [Fact]
        public void ShouldReturnNull()
        {
            //Act

            //Arrange
            Assert.Null(customer.GreetMessage);
        }

        [Fact]
        public void ShouldReturnValidDiscount()
        {
            //Act
            int actual = customer.Discount;

            //Assert
            Assert.InRange(actual, 10, 25);
        }

        [Fact]
        public void ShouldThrowFisrtNameNullExceptionWithMesage()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                customer.GetFullName("", "Doe");
            });

            Assert.Equal("Empty First Name", exception?.Message);

            Assert.Throws<ArgumentException>(() => customer.GetFullName("", "Doe"));
        }

        [Fact]
        public void ShouldReturnCustomerTypeForOrderTotalLessThan100()
        {
            customer.OrderTotal = 80;

            var actual = customer.GetCustomerDetails();

            Assert.IsType<BasicCustomer>(actual);
        }

        [Fact]
        public void ShouldReturnCustomerTypeForOrderTotalMoreThan100()
        {
            customer.OrderTotal = 120;

            var actual = customer.GetCustomerDetails();

            Assert.IsType<PlatinumCustomer>(actual);
        }
    }
}
