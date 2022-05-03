using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using unit_testing_00;

namespace unit_test_00
{
    [TestFixture]
    public class ProductNUnitTests
    {
        [Test]
        public void ShouldReturnPriceWithDiscountForPlatinumCustomer()
        {
            Product product = new() { Price = 50 };

            var result = product.GetPrice(new Customer() { IsPlatinum = true });

            Assert.That(result, Is.EqualTo(40));
        }
        [Test]
        public void ShouldReturnPriceWithDiscountForPlatinumCustomer_WithMock()
        {
            Product product = new() { Price = 50 };

            var customer = new Mock<ICustomer>();
            customer.Setup(c => c.IsPlatinum).Returns(true);

            var result = product.GetPrice(customer.Object);

            Assert.That(result, Is.EqualTo(40));
        }
    }
}
