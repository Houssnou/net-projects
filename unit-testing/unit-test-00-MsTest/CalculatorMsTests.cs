using Microsoft.VisualStudio.TestTools.UnitTesting;
using unit_testing_00;

namespace unit_test_00_MsTest;

[TestClass]
public class CalculatorMsTests
{
    [TestMethod]
    public void ShouldAddTwoNumbers()
    {
        //Arrange
        Calculator calculator = new();

        //Act
        int result = calculator.Add(10, 20);

        //Assert
        Assert.AreEqual(30, result);
    }

}