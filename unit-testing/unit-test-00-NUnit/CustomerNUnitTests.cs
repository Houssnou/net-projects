using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using unit_testing_00;

namespace unit_test_00
{
    public class CustomerNUnitTests
    {
        private Customer customer;

        [SetUp]
        public void SetUp()
        {
            customer = new Customer();
        }
        [Test]
        public void ShouldReturnCustomerFullName()
        {
            //using SetUp instead of repeating Class initialization in each test
            ////Arrange
            //Customer customer = new(); 



            //Act
            string fullName = customer.GetFullName("John", "Doe");

            //Assert
            Assert.That(fullName, Is.EqualTo("Hello, John Doe"));
            Assert.That(fullName, Does.Contain("John"));
            Assert.That(fullName, Does.StartWith("Hello"));
            Assert.That(fullName, Does.EndWith("Doe"));

            Assert.AreEqual("Hello, John Doe", fullName);

            Assert.That(fullName, Does.Match("Hello, [A-Z]{1}[a-z]+ "));
        }

        [Test]
        public void ShouldReturnCustomerFullNameWithMultipleAssert()
        {
            //Act
            string fullName = customer.GetFullName("John", "Doe");

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(fullName, Is.EqualTo("Hello, John Doe"));
                Assert.That(fullName, Does.Contain("John"));
                Assert.That(fullName, Does.StartWith("Hello"));
                Assert.That(fullName, Does.EndWith("Doe"));

                Assert.AreEqual("Hello, John Doe", fullName);

                Assert.That(fullName, Does.Match("Hello, [A-Z]{1}[a-z]+ "));
            });
        }

        [Test]
        public void ShouldReturnNull()
        {
            //using SetUp instead of repeating Class initialization in each test
            ////Arrange
            //Customer customer = new();

            //Act

            //Arrange
            Assert.IsNull(customer.GreetMessage);
        }

        [Test]
        public void ShouldReturnValidDiscount()
        {
            //Act
            int result = customer.Discount;

            //Assert
            Assert.That(result, Is.InRange(10, 25));
        }

        [Test]
        public void ShouldThrowFisrtNameNullExceptionWithMesage()
        {
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                customer.GetFullName("", "Doe");
            });

            Assert.AreEqual("Empty First Name", exception?.Message);

            Assert.That(() => customer.GetFullName("", "Doe"), Throws.ArgumentException.With.Message.EqualTo("Empty First Name"));
        }

        [Test]
        public void ShouldThrowFirstNameArgumentException()
        {
            var exepection = Assert.Throws<ArgumentException>(() =>
            {
                customer.GetFullName("", "Doe");
            });

            Assert.That(() => customer.GetFullName("", "Doe"), Throws.ArgumentException);
        }

        [Test]
        public void ShouldReturnCustomerTypeForOrderTotalLessThan100()
        {
            customer.OrderTotal = 80;

            var result = customer.GetCustomerDetails();

            Assert.That(result, Is.TypeOf<BasicCustomer>());
        }

        [Test]
        public void ShouldReturnCustomerTypeForOrderTotalMoreThan100()
        {
            customer.OrderTotal = 120;

            var result = customer.GetCustomerDetails();

            Assert.That(result, Is.TypeOf<PlatinumCustomer>());
        }


    }
}
