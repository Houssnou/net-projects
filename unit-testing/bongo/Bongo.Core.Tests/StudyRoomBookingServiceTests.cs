using System;
using System.Collections.Generic;
using System.Linq;
using Bongo.Core.Services;
using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Bongo.Core.Tests
{
    [TestFixture]
    public class StudyRoomBookingServiceTests
    {
        private Mock<IStudyRoomBookingRepository> _studyRoomBookingRepoMock;
        private Mock<IStudyRoomRepository> _studyRoomRepoMock;
        private StudyRoomBookingService _bookService;
        private StudyRoomBooking _request;
        private List<StudyRoom> _availablesStudyRoom;

        [SetUp]
        public void Setup()
        {
            _request = new()
            {
                FirstName = "John1",
                LastName = "Doe1",
                Date = new DateTime(2023, 1, 11),
                Email = "joedoe1@gmail.coom"
            };

            _availablesStudyRoom = new List<StudyRoom>()
            {
                new ()
                {
                    Id = 10,
                    RoomName = "Presidential",
                    RoomNumber = "A202"
                }
            };
            _studyRoomBookingRepoMock = new Mock<IStudyRoomBookingRepository>();

            _studyRoomRepoMock = new Mock<IStudyRoomRepository>();
            _studyRoomRepoMock.Setup(r => r.GetAll()).Returns(_availablesStudyRoom);

            _bookService = new StudyRoomBookingService(_studyRoomBookingRepoMock.Object, _studyRoomRepoMock.Object);
        }

        [Test]
        public void Should_InvokeRepository_ForGetAllBooking()
        {
            _bookService.GetAllBooking();
            _studyRoomBookingRepoMock.Verify(x => x.GetAll(null), Times.Once);
        }

        [Test]
        public void Should_ThrowsArgumentNullException_ForNullStudyRoomBooking()
        {
            var exception = Assert.Throws<ArgumentNullException>((() => _bookService.BookStudyRoom(null)));

            //optional
            Assert.IsNotNull(exception);
            Assert.AreEqual("Value cannot be null. (Parameter 'request')", exception.Message);
            Assert.AreEqual("request", exception.ParamName);
        }

        [Test]
        public void Should_SaveBookingAndsReturnResultValues()
        {
            //arrange
            StudyRoomBooking createdStudyRoomBooking = null;

            _studyRoomBookingRepoMock
                .Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
                .Callback<StudyRoomBooking>(booking => { createdStudyRoomBooking = booking; });

            //act
            _bookService.BookStudyRoom(_request);

            //assert
            _studyRoomBookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Once);

            Assert.NotNull(createdStudyRoomBooking);

            Assert.AreEqual(_request.FirstName, createdStudyRoomBooking.FirstName);
            Assert.AreEqual(_request.LastName, createdStudyRoomBooking.LastName);
            Assert.AreEqual(_request.Email, createdStudyRoomBooking.Email);
            Assert.AreEqual(_request.Date, createdStudyRoomBooking.Date);
            Assert.AreEqual(_availablesStudyRoom.First().Id, createdStudyRoomBooking.StudyRoomId);
        }

        [Test]
        public void Should_ReturnExactValues_ForBookingRquest()
        {
            var actual = _bookService.BookStudyRoom(_request);

            Assert.NotNull(actual);
            Assert.AreEqual(_request.FirstName, actual.FirstName);
            Assert.AreEqual(_request.LastName, actual.LastName);
            Assert.AreEqual(_request.Email, actual.Email);
            Assert.AreEqual(_request.Date, actual.Date);
        }

        [Test]
        public void Should_ReturnSuccessRoomAvailability_ForAvailableRoomRequest()
        {
            var actual = _bookService.BookStudyRoom(_request);

            Assert.AreEqual(StudyRoomBookingCode.Success, actual.Code);
        }

        [TestCase(true, ExpectedResult = StudyRoomBookingCode.Success)]
        [TestCase(false, ExpectedResult = StudyRoomBookingCode.NoRoomAvailable)]
        public StudyRoomBookingCode Should_ReturnRoomAvailability_ForBookinRequest(bool availability)
        {
            if (!availability)
            {
                _availablesStudyRoom.Clear();
            }
            var actual = _bookService.BookStudyRoom(_request);

            return actual.Code;
        }

        [TestCase(0, false)]
        [TestCase(1, true)] // any valid id
        public void Should_SaveRoomBookingAndBookingId_ForAvailability(int expectedId, bool roomAvailability)
        {
            //arrange
            if (!roomAvailability)
            {
                _availablesStudyRoom.Clear();
            }

            _studyRoomBookingRepoMock
                .Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
                .Callback<StudyRoomBooking>(booking => { booking.BookingId = 1; });

            //act
            var actual = _bookService.BookStudyRoom(_request);

            //assert
            Assert.AreEqual(expectedId, actual.BookingId);
        }

        [Test]
        public void Should_NotInvokeBookingMethod_ForCreatingBookWithoutAvailableRoom()
        {
            //arrange
            _availablesStudyRoom.Clear();

            //act
            var actual = _bookService.BookStudyRoom(_request);

            //assert
            _studyRoomBookingRepoMock.Verify(b => b.Book(It.IsAny<StudyRoomBooking>()), Times.Never);
        }
    }
}