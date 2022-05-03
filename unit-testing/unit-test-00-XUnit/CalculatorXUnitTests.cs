using System.Collections.Generic;
using System.Linq;
using unit_testing_00;
using Xunit;

namespace unit_test_00_XUnit
{
    public class CalculatorXUnitTests
    {
        public Calculator calculator;

        public CalculatorXUnitTests()
        {
            calculator = new();
        }

        [Fact]
        public void ShouldAddTwoNumbers()
        {
            //Arrange
            //Act
            int result = calculator.Add(10, 20);

            //Assert
            Assert.Equal(30, result);
        }

        [Theory]
        [InlineData(5.4, 10.5)] //15.9
        [InlineData(5.43, 10.53)] //15.96
        public void ShouldAddTwoDoublesNumber(double a, double b)
        {
            //Arrange
            //Calculator calculator = new Calculator();

            //Act
            double result = calculator.Add(a, b);

            //Assert
            Assert.Equal(15.9, result, 0);
        }

        [Fact]
        public void ShouldReturnTrueForOddNumber()
        {
            //Arrange
            //Calculator calculator = new();
            //Act
            bool result = calculator.IsOddNumber(3);
            //Assert
            Assert.True(result);
        }
        [Fact]
        public void ShouldReturnFalseForOddNumber()
        {
            //Arrange
            //Calculator calculator = new();
            //Act
            bool result = calculator.IsOddNumber(2);
            //Assert
            Assert.False(result);
        }
        [Theory]
        //[InlineData(11)]
        [InlineData(12)]
        public void ShouldReturnFalseForInputOddNumber(int a)
        {
            //Arrange
            //Calculator calculator = new();
            //Act
            bool result = calculator.IsOddNumber(a);
            //Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(11, true)]
        [InlineData(12, false)]
        public void ShouldReturnFalseForInputOddNumber_WithExpectedResult(int a, bool expectedResult)
        {
            //Arrange
            //Calculator calculator = new();

            //Act
            bool result = calculator.IsOddNumber(a);

            //Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void ShouldReturnValidOddNumbersWithinMinAndMax()
        {
            //Arrange
            List<int> expected = new() { 5, 7, 9 }; //5-10

            //Act
            List<int> result = calculator.GetOddRange(5, 10);

            //Assert
            Assert.Equal(expected, result);
            Assert.Contains(7, result);
            Assert.NotEmpty(result);
            Assert.Equal(3, result.Count);
            Assert.DoesNotContain(6,result);
            Assert.Equal(result.OrderBy(u=>u), result);
        }
    }
}
