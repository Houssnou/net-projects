using Moq;
using unit_testing_00;
using Xunit;

namespace unit_Fact_00_XUnit
{
   public class ProductXUnitFacts
    {
        [Fact]
        public void ShouldReturnPriceWithDiscountForPlatinumCustomer()
        {
            Product product = new() { Price = 50 };

            var result = product.GetPrice(new Customer() { IsPlatinum = true });

            Assert.Equal(40, result);
        }

        [Fact]
        public void ShouldReturnPriceWithDiscountForPlatinumCustomer_WithMock()
        {
            Product product = new() { Price = 50 };

            var customer = new Mock<ICustomer>();
            customer.Setup(c => c.IsPlatinum).Returns(true);

            var result = product.GetPrice(customer.Object);

            Assert.Equal(40, result);
        }
    }
}
