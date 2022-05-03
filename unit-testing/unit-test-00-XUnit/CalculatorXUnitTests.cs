using System.Collections.Generic;

namespace unit_test_00_XUnit
{
    [TestFixture]
    public class CalculatorXUnitTests
    {
        public Calculator calculator;
        [SetUp]
        public void SetUp()
        {
            calculator = new();
        }

        [Test]
        public void ShouldAddTwoNumbers()
        {
            //Arrange
            //Calculator calculator = new();

            //Act
            int result = calculator.Add(10, 20);

            //Assert
            Assert.AreEqual(30, result);
        }

        [Test]
        [TestCase(5.4, 10.5)] //15.9
        [TestCase(5.43, 10.53)] //15.96
        public void ShouldAddTwoDoublesNumber(double a, double b)
        {
            //Arrange
            Calculator calculator = new Calculator();

            //Act
            double result = calculator.Add(a, b);

            //Assert
            Assert.AreEqual(15.9, result, 1);
        }

        [Test]
        public void ShouldReturnTrueForOddNumber()
        {
            //Arrange
            //Calculator calculator = new();
            //Act
            bool result = calculator.IsOddNumber(3);
            //Assert
            Assert.IsTrue(result);
        }
        [Test]
        public void ShouldReturnFalseForOddNumber()
        {
            //Arrange
            //Calculator calculator = new();
            //Act
            bool result = calculator.IsOddNumber(2);
            //Assert
            Assert.IsFalse(result);
        }
        [Test]
        //[TestCase(11)]
        [TestCase(12)]
        public void ShouldReturnFalseForInputOddNumber(int a)
        {
            //Arrange
            //Calculator calculator = new();
            //Act
            bool result = calculator.IsOddNumber(a);
            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        [TestCase(11, ExpectedResult = true)]
        [TestCase(12, ExpectedResult = false)]
        public bool ShouldReturnFalseForInputOddNumber_WithExpectedResult(int a)
        {
            //Arrange
            //Calculator calculator = new();

            //Act
            bool result = calculator.IsOddNumber(a);

            //Assert
            return result;
        }

        [Test]
        public void ShouldReturnValidOddNumbersWithinMinAndMax()
        {
            //Arrange
            List<int> expected = new() { 5, 7, 9 }; //5-10

            //Act
            List<int> result = calculator.GetOddRange(5, 10);

            //Assert
            Assert.That(result, Is.EquivalentTo(expected));
            Assert.AreEqual(expected, result);
            Assert.Contains(7, result);
            Assert.That(result, Does.Contain(7));
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Has.No.Member(6));
            Assert.That(result, Is.Ordered);
            Assert.That(result, Is.Unique);
        }
    }
}
