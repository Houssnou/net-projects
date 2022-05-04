using System;
using Bongo.Core.Services.IServices;
using Bongo.Models.Model;
using Bongo.Models.Model.VM;
using Bongo.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace Bongo.Web.Tests;

[TestFixture]
public class RoomBookingControllerTests
{
    private Mock<IStudyRoomBookingService> _studyRoomBookingService;
    private RoomBookingController _bookingController;

    [SetUp]
    public void Setup()
    {
        _studyRoomBookingService = new Mock<IStudyRoomBookingService>();
        _bookingController = new RoomBookingController(_studyRoomBookingService.Object);

    }

    [Test]
    public void Should_VerifyGetAllInvoked_ForIndexPage()
    {
        _bookingController.Index();
        _studyRoomBookingService.Verify(x => x.GetAllBooking(), Times.Once);
    }

    [Test]
    public void Should_ReturnBackToBookingPage_ForInvalidModelState()
    {
        _bookingController.ModelState.AddModelError("UnitTest", "Invalid model");

        var actual = _bookingController.Book(new StudyRoomBooking());

        Assert.That(actual, Is.Not.Null);

        ViewResult view = actual as ViewResult;
        Assert.AreEqual("Book", view.ViewName);
    }

    [Test]
    public void Should_ReturnNoRoomCode_ForFailedRoomBooking()
    {
        _studyRoomBookingService
            .Setup(x => x.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
            .Returns(new StudyRoomBookingResult()
            {
                Code = StudyRoomBookingCode.NoRoomAvailable
            });

        var actual = _bookingController.Book(new StudyRoomBooking());

        Assert.IsInstanceOf<ViewResult>(actual);

        ViewResult view = actual as ViewResult;
        Assert.AreEqual("No Study Room available for selected date", view.ViewData["Error"]);
    }

    [Test]
    public void Should_RedirectToActionWithSuccessCode_ForCreatedRoomBooking()
    {
        //arrange
        _studyRoomBookingService
            .Setup(x => x.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
            .Returns((StudyRoomBooking booking) => new StudyRoomBookingResult()
            {
                Code = StudyRoomBookingCode.Success,
                FirstName = booking.FirstName,
                LastName = booking.LastName,
                Date = booking.Date,
                Email = booking.Email
            });

        //act
        var actual = _bookingController.Book(new StudyRoomBooking()
        {
            FirstName = "John1",
            LastName = "Doe1",
            Date = new DateTime(2023, 1, 11),
            Email = "joedoe1@gmail.coom",
            BookingId = 1,
            StudyRoomId = 1
        });

        //assert
        Assert.IsInstanceOf<RedirectToActionResult>(actual);

        RedirectToActionResult actionResult = actual as RedirectToActionResult;
        Assert.AreEqual("John1", actionResult.RouteValues["FirstName"]);
        Assert.AreEqual(StudyRoomBookingCode.Success, actionResult.RouteValues["Code"]);
    }
}