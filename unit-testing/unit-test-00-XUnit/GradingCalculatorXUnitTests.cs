using unit_testing_00;
using Xunit;

namespace unit_test_00_XUnit
{

    public class GradingCalculatorXUnitTests
    {
        private GradingCalculator gradingCalculator;

        public GradingCalculatorXUnitTests()
        {
            gradingCalculator = new();
        }
        [Fact]
        public void ShouldReturnGradeA()
        {
            //Arrange
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 90;

            //Act
            var grade = gradingCalculator.GetGrade();

            //Assert
            Assert.Equal("A", grade);
        }

        [Fact]
        public void ShoudReturnGradeB()
        {
            //Arrange
            gradingCalculator.Score = 85;
            gradingCalculator.AttendancePercentage = 90;

            //Act
            var grade = gradingCalculator.GetGrade();

            //Assert
            Assert.Equal("B", grade);
        }

        [Fact]
        public void ShouldReturnGradeC()
        {
            //Arrange
            gradingCalculator.Score = 65;
            gradingCalculator.AttendancePercentage = 90;

            //Act
            var grade = gradingCalculator.GetGrade();

            //Assert
            Assert.Equal("C", grade);
        }

        [Fact]
        public void ShouldReturnB_ForInputs()
        {
            //Arrange
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 65;

            //Act
            var grade = gradingCalculator.GetGrade();

            //Assert
            Assert.Equal("B", grade);
        }

        [Theory]
        [InlineData(95, 55, "F")]
        [InlineData(65, 55, "F")]
        [InlineData(50, 90, "F")]
        public void ShouldReturnFGradeForMultipleInputs(int score, int attendance, string expectedGrade)
        {
            //Arrange
            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attendance;

            //Act
            var grade = gradingCalculator.GetGrade();

            //Assert
            Assert.Equal(expectedGrade, grade);
        }

        [Theory]
        [InlineData(95, 90, "A")]
        [InlineData(85, 90, "B")]
        [InlineData(65, 90, "C")]
        [InlineData(95, 65, "B")]

        [InlineData(95, 55, "F")]
        [InlineData(65, 55, "F")]
        [InlineData(50, 90, "F")]
        public void ShouldReturnValidGradesForMultipleInputs(int score, int attendance, string expectedGrade)
        {
            //Arrange
            gradingCalculator.Score = score;
            gradingCalculator.AttendancePercentage = attendance;

            //Act
            var grade = gradingCalculator.GetGrade();

            //Assert
            Assert.Equal(expectedGrade, grade);
        }
    }
}
