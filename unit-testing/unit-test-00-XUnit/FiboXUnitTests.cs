using System.Collections.Generic;
using System.Linq;
using unit_testing_00;
using Xunit;

namespace unit_test_00_XUnit
{
    public class FiboXUnitTests
    {
        private Fibo fibo;

        public FiboXUnitTests()
        {
            fibo = new();
        }

        [Fact]
        public void ShouldReturnNotEmptyOrdederedList_ForInput1()
        {
            //Arrange
            fibo.Range = 1;
            List<int> expected = new() { 0 };

            //Act
            var result = fibo.GetFiboSeries();

            //Assert
            Assert.NotEmpty(result);
            Assert.Equal(expected.OrderBy(x => x), result);
            Assert.Equal(expected, result);
            Assert.True(result.SequenceEqual(expected));
        }

        [Fact]
        public void ShouldReturnNotEmptyList_ForInput6()
        {
            //Arrange
            fibo.Range = 6;
            List<int> expected = new() { 0, 1, 1, 2, 3, 5 };
            int expectedCount = 6;

            //Act
            var result = fibo.GetFiboSeries();
            
            Assert.Contains(3, result);
            Assert.Equal(expectedCount, result.Count);
            Assert.NotEmpty(result);
            Assert.DoesNotContain(4, result);
            Assert.NotEmpty(result);
            Assert.Equal(expected.OrderBy(x => x), result);
            Assert.Equal(expected, result);
        }
    }
}
