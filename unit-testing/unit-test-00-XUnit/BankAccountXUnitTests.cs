using Moq;
using unit_testing_00;
using Xunit;

namespace unit_test_00_XUnit
{
    public class BankAccountXUnitTests
    {
        [Fact]
        public void ShouldReturnTrueForDeposit()
        {
            // BankAccount bankAccountWithActualWithDependency = new(new LogBook());
            BankAccount bankAccountWithFaker = new(new LogFaker());

            var actual = bankAccountWithFaker.Deposit(100);

            Assert.True(actual);
            Assert.Equal(100, bankAccountWithFaker.GetBalance());
        }

        [Fact]
        public void ShouldReturnTrueForDepositWithMock()
        {
            var logMock = new Mock<ILogBook>();

            BankAccount bankAccountWithMock = new(logMock.Object);

            var actual = bankAccountWithMock.Deposit(100);

            Assert.True(actual);
            Assert.Equal(100, bankAccountWithMock.GetBalance());
        }

        [Fact]
        public void ShouldReturnTrueFor100WithdramWith200Balance()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.IsAny<decimal>())).Returns(true);

            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(200);

            var actual = bankAccount.Withdrawn(300);

            Assert.True(actual);

        }

        [Theory]
        [InlineData(200, 100)]
        [InlineData(200, 150)]
        public void ShouldReturnTrueFor100WithdramWith200Balance_Theory(decimal balance, decimal withdraw)
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<decimal>(x => x > 0))).Returns(true);

            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(balance);

            var actual = bankAccount.Withdrawn(withdraw);

            Assert.True(actual);
        }

        [Theory]
        [InlineData(200, 300)]
        [InlineData(200, 350)]
        public void ShouldReturnFalseFor300WithdrawWith200Balance(decimal balance, decimal withdraw)
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<decimal>(x => x > 0))).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<decimal>(x => x < 0))).Returns(false);
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.IsInRange(decimal.MinValue, -1, Moq.Range.Inclusive))).Returns(false);

            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(balance);

            var actual = bankAccount.Withdrawn(withdraw);

            Assert.False(actual);
        }

        [Fact]
        public void ShouldReturnMessage()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(u => u.LogWithStringReturn(It.IsAny<string>())).Returns((string str) => str.ToLower());


            var expectedOutpout = "hello";
            var actual = logMock.Object.LogWithStringReturn("HeLlo");

            Assert.Equal(expectedOutpout, actual);
        }

        [Fact]
        public void ShouldTrueWithOutputMessage()
        {
            var logMock = new Mock<ILogBook>();
            var expectedOutpout = "hello";
            logMock.Setup(x => x.LogWithBooleanOutputResult(It.IsAny<string>(), out expectedOutpout)).Returns(true);

            var actual = "";

            Assert.True(logMock.Object.LogWithBooleanOutputResult("John", out actual));
            Assert.Equal(expectedOutpout, actual);
        }

        [Fact]
        public void ShouldReturnTrueForRefObject()
        {
            var logMock = new Mock<ILogBook>();

            Customer customer = new();
            Customer customerNotUsed = new();

            logMock.Setup(u => u.LogWithRefObject(ref customer)).Returns(true);

            Assert.True(logMock.Object.LogWithRefObject(ref customer));
            Assert.False(logMock.Object.LogWithRefObject(ref customerNotUsed));
        }

        [Fact]
        public void ShouldSetLogTypeAndSeverity()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.LogSeverity).Returns(10);
            logMock.Setup(x => x.LogType).Returns("Warning");

            //logMock.SetupProperty(p=>p.LogSeverity);
            //logMock.Object.LogSeverity = 100;

            //Assert.That(logMock.Object.LogSeverity, Is.EqualTo(100));
            Assert.Equal(10, logMock.Object.LogSeverity);
            Assert.Equal("Warning", logMock.Object.LogType);
        }

        [Fact]
        public void ShouldSetLogTypeAndSeverityWithCallback()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.LogSeverity).Returns(10);
            logMock.Setup(x => x.LogType).Returns("Warning");

            Assert.Equal(10, logMock.Object.LogSeverity);
            Assert.Equal("Warning", logMock.Object.LogType);
            //callbacks
            string logTemp = "Hello, ";
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true).Callback((string str) => logTemp += str);

            logMock.Object.LogToDb("John");

            Assert.Equal("Hello, John", logTemp);

            //callbacks
            int counter = 5;
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true).Callback(() => counter++);

            logMock.Object.LogToDb("John");
            logMock.Object.LogToDb("John");

            Assert.Equal(7, counter);

            //callbacks
            int beforeAndAfterCounter = 5;
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Callback(() => beforeAndAfterCounter++).Returns(true).Callback(() => beforeAndAfterCounter++);

            logMock.Object.LogToDb("John");
            logMock.Object.LogToDb("John");

            Assert.Equal(9, beforeAndAfterCounter);
        }

        [Fact]
        public void ShouldReturnHowManyTimesAMethodWasInvoked()
        {
            var logMock = new Mock<ILogBook>();

            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(100);

            Assert.Equal(100, bankAccount.GetBalance());

            logMock.Verify(x => x.Message(It.IsAny<string>()), Times.Exactly(2));
            logMock.Verify(x => x.Message("Test"), Times.AtLeastOnce);
            // logMock.Verify(x => x.LogSeverity, Times.Once);
            logMock.VerifySet(x => x.LogSeverity = 100, Times.Once);
            logMock.VerifyGet(x => x.LogSeverity, Times.Once);
        }
    }
}
