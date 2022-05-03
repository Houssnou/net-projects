using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using unit_testing_00;

namespace unit_test_00
{

    public class GradingCalculatorNUnitTests
    {
        private GradingCalculator gradingCalculator;

        [SetUp]
        public void SetUp()
        {
            gradingCalculator = new();
        }

        [Test]
        public void ShouldReturnGradeA()
        {
            //Arrange
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 90;

            //Act
            var grade = gradingCalculator.GetGrade();

            //Assert
            Assert.That(grade, Is.EqualTo("A"));
        }

        [Test]
        public void ShoudReturnGradeB()
        {
            //Arrange
            gradingCalculator.Score = 85;
            gradingCalculator.AttendancePercentage = 90;

            //Act
            var grade = gradingCalculator.GetGrade();

            //Assert
            Assert.That(grade, Is.EqualTo("B"));
        }

        [Test]
        public void ShouldReturnGradeC()
        {
            //Arrange
            gradingCalculator.Score = 65;
            gradingCalculator.AttendancePercentage = 90;

            //Act
            var grade = gradingCalculator.GetGrade();

            //Assert
            Assert.That(grade, Is.EqualTo("C"));
        }

        [Test]
        public void ShouldReturnB_ForInputs()
        {
            //Arrange
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 65;

            //Act
            var grade = gradingCalculator.GetGrade();

            //Assert
            Assert.That(grade, Is.EqualTo("B"));
        }

        [Test]
        [TestCase(95, 55)]
        [TestCase(65, 55)]
        [TestCase(50, 90)]
        public void ShouldReturnFGradeForMultipleInputs(int score, int attendance)
        {
            //Arrange
            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attendance;

            //Act
            var grade = gradingCalculator.GetGrade();

            //Assert
            Assert.That(grade, Is.EqualTo("F"));
        }


        
        [Test]
        [TestCase(95, 90, ExpectedResult = "A")]
        [TestCase(85, 90, ExpectedResult = "B")]
        [TestCase(65, 90, ExpectedResult = "C")]
        [TestCase(95, 65, ExpectedResult = "B")]

        [TestCase(95, 55, ExpectedResult = "F")]
        [TestCase(65, 55, ExpectedResult = "F")]
        [TestCase(50, 90, ExpectedResult = "F")]
        public string ShouldReturnValidGradesForMultipleInputs(int score, int attendance)
        {
            //Arrange
            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attendance;

            //Act
            var grade = gradingCalculator.GetGrade();

            //Assert
            return grade;
        }


    }
}
