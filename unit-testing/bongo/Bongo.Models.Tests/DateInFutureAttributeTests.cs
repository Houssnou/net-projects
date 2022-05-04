using System;
using Bongo.Models.ModelValidations;
using NUnit.Framework;

namespace Bongo.Models.Tests
{
    [TestFixture]
    public class DateInFutureAttributeTests
    {
        [Test]
        public void Should_ReturnValidDate()
        {
            DateInFutureAttribute dateInFutureAttribute = new(() => DateTime.Now);

            var actual = dateInFutureAttribute.IsValid(DateTime.Now.AddSeconds(-200));
            Assert.That(actual, Is.True);
        }

        [Test]
        [TestCase(150, ExpectedResult = true)]
        [TestCase(-100, ExpectedResult = false)]
        public bool Should_ReturnDateValidity_ForMultipleInputs(int timeinSeconds)
        {
            DateInFutureAttribute dateInFutureAttribute = new(() => DateTime.Now);

            return dateInFutureAttribute.IsValid(DateTime.Now.AddSeconds(timeinSeconds));
        }

        [Test]
        public void Should_ReturnErrorMessage_ForInvalidDate()
        {
            var actual = new DateInFutureAttribute();

            Assert.AreEqual("Date must be in the future", actual.ErrorMessage);
        }
    }
}
