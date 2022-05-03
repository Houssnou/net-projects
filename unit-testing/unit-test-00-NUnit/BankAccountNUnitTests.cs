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
    public class BankAccountNUnitTests
    {
        private BankAccount bankAccount;

        [SetUp]
        public void SetUp()
        {

        }

        //[Test]
        //public void ShouldReturnTrueForDeposit()
        //{
        //    // BankAccount bankAccountWithActualWithDependency = new(new LogBook());
        //    BankAccount bankAccountWithFaker = new(new LogFaker());

        //    var result = bankAccountWithFaker.Deposit(100);

        //    Assert.True(result);
        //    Assert.That(bankAccountWithFaker.GetBalance(), Is.EqualTo(100));
        //}

        [Test]
        public void ShouldReturnTrueForDepositWithMock()
        {
            var logMock = new Mock<ILogBook>();

            BankAccount bankAccountWithMock = new(logMock.Object);

            var result = bankAccountWithMock.Deposit(100);

            Assert.True(result);
            Assert.That(bankAccountWithMock.GetBalance(), Is.EqualTo(100));
        }

        //[Test]
        //public void ShouldReturnTrueFor100WithdramWith200Balance()
        //{
        //    var logMock = new Mock<ILogBook>();
        //    logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
        //    logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.IsAny<decimal>())).Returns(true);

        //    BankAccount bankAccount = new(logMock.Object);
        //    bankAccount.Deposit(200);

        //    var result = bankAccount.Withdrawn(300);

        //    Assert.IsTrue(result);

        //}

        [Test]
        [TestCase(200, 100)]
        [TestCase(200, 150)]
        public void ShouldReturnTrueFor100WithdramWith200Balance(decimal balance, decimal withdraw)
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<decimal>(x => x > 0))).Returns(true);

            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(balance);

            var result = bankAccount.Withdrawn(withdraw);

            Assert.IsTrue(result);

        }

        [Test]
        [TestCase(200, 300)]
        [TestCase(200, 350)]
        public void ShouldReturnTrueFor300WithdramWith200Balance(decimal balance, decimal withdraw)
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<decimal>(x => x > 0))).Returns(true);
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.Is<decimal>(x => x < 0))).Returns(false);
            logMock.Setup(u => u.LogBalanceAfterWithdrawal(It.IsInRange(decimal.MinValue, -1, Moq.Range.Inclusive))).Returns(false);

            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(balance);

            var result = bankAccount.Withdrawn(withdraw);

            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldReturnMessage()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(u => u.LogWithStringReturn(It.IsAny<string>())).Returns((string str) => str.ToLower());


            var expectedOutpout = "hello";
            var result = logMock.Object.LogWithStringReturn("HeLlo");

            Assert.That(result, Is.EqualTo(expectedOutpout));
        }

        [Test]
        public void ShouldTrueWithOutputMessage()
        {
            var logMock = new Mock<ILogBook>();
            var expectedOutpout = "hello";
            logMock.Setup(x => x.LogWithBooleanOutputResult(It.IsAny<string>(), out expectedOutpout)).Returns(true);

            var result = "";

            Assert.IsTrue(logMock.Object.LogWithBooleanOutputResult("John", out result));
            Assert.That(result, Is.EqualTo(expectedOutpout));
        }

        [Test]
        public void ShouldReturnTrueForRefObject()
        {
            var logMock = new Mock<ILogBook>();

            Customer customer = new();
            Customer customerNotUsed = new();

            logMock.Setup(u => u.LogWithRefObject(ref customer)).Returns(true);

            Assert.IsTrue(logMock.Object.LogWithRefObject(ref customer));
            Assert.IsFalse(logMock.Object.LogWithRefObject(ref customerNotUsed));
        }

        [Test]
        public void ShouldSetLogTypeAndSeverity()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.LogSeverity).Returns(10);
            logMock.Setup(x => x.LogType).Returns("Warning");

            //logMock.SetupProperty(p=>p.LogSeverity);
            //logMock.Object.LogSeverity = 100;

            //Assert.That(logMock.Object.LogSeverity, Is.EqualTo(100));
            Assert.That(logMock.Object.LogSeverity, Is.EqualTo(10));
            Assert.That(logMock.Object.LogType, Is.EqualTo("Warning"));
        }

        [Test]
        public void ShouldSetLogTypeAndSeverityWithCallback()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.LogSeverity).Returns(10);
            logMock.Setup(x => x.LogType).Returns("Warning");

            Assert.That(logMock.Object.LogSeverity, Is.EqualTo(10));
            Assert.That(logMock.Object.LogType, Is.EqualTo("Warning"));

            //callbacks
            string logTemp = "Hello, ";
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true).Callback((string str) => logTemp += str);

            logMock.Object.LogToDb("John");

            Assert.That(logTemp, Is.EqualTo("Hello, John"));

            //callbacks
            int counter = 5;
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Returns(true).Callback(() => counter++);

            logMock.Object.LogToDb("John");
            logMock.Object.LogToDb("John");

            Assert.That(counter, Is.EqualTo(7));

            //callbacks
            int beforeAndAfterCounter = 5;
            logMock.Setup(u => u.LogToDb(It.IsAny<string>())).Callback(() => beforeAndAfterCounter++).Returns(true).Callback(() => beforeAndAfterCounter++);

            logMock.Object.LogToDb("John");
            logMock.Object.LogToDb("John");

            Assert.That(beforeAndAfterCounter, Is.EqualTo(9));
        }

        [Test]
        public void ShouldReturnHowManyTimesAMethodWasInvoked()
        {
            var logMock = new Mock<ILogBook>();

            BankAccount bankAccount = new(logMock.Object);
            bankAccount.Deposit(100);

            Assert.That(bankAccount.GetBalance(), Is.EqualTo(100));

            logMock.Verify(x => x.Message(It.IsAny<string>()), Times.Exactly(2));
            logMock.Verify(x => x.Message("Test"), Times.AtLeastOnce);
            // logMock.Verify(x => x.LogSeverity, Times.Once);
            logMock.VerifySet(x => x.LogSeverity = 100, Times.Once);
            logMock.VerifyGet(x => x.LogSeverity , Times.Once);
        }
    }
}
