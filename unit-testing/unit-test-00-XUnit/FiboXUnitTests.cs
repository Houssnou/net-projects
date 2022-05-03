using System.Collections.Generic;

namespace unit_test_00_XUnit
{
    public class FiboXUnitTests
    {
        private Fibo fibo;

        [SetUp]
        public void SetUp()
        {
            fibo = new();
        }

        [Test]
        public void ShouldReturnNotEmptyOrdederedList_ForInput1()
        {
            //Arrange
            fibo.Range = 1;
            List<int> expected = new() { 0 };

            //Act
            var result = fibo.GetFiboSeries();

            //Assert
            Assert.That(result, Is.Not.Empty);
            Assert.That(result, Is.Ordered);
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void ShouldReturnNotEmptyList_ForInput6()
        {
            //Arrange
            fibo.Range = 6;
            List<int> expected = new() { 0, 1, 1, 2, 3, 5 };
            int expectedCount = 6;

            //Act
            var result = fibo.GetFiboSeries(); 

            //Assert
            Assert.That(result, Does.Contain(3));
            Assert.That(result.Count, Is.EqualTo(result.Count));
            Assert.That(result.Count, Is.EqualTo(6));
            Assert.That(result, Does.Not.Contain(4));
            Assert.That(result, Is.EquivalentTo(expected));
        }
    }
}
