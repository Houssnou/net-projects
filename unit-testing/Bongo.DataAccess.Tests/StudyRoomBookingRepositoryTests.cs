using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Bongo.DataAccess.Repository;
using Bongo.Models.Model;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Bongo.DataAccess.Tests;

[TestFixture]
public class StudyRoomBookingRepositoryTests
{
    private StudyRoomBooking studyRoomBookingOne;
    private StudyRoomBooking studyRoomBookingTwo;
    public readonly DbContextOptions<ApplicationDbContext> options;
    public StudyRoomBookingRepositoryTests()
    {
        studyRoomBookingOne = new()
        {
            FirstName = "John1",
            LastName = "Doe1",
            Date = new DateTime(2023, 1, 11),
            Email = "joedoe1@gmail.coom",
            BookingId = 11,
            StudyRoomId = 1
        };

        studyRoomBookingTwo = new()
        {
            FirstName = "John2",
            LastName = "Doe2",
            Date = new DateTime(2024, 10, 1),
            Email = "joedoe2@gmail.coom",
            BookingId = 22,
            StudyRoomId = 2
        };

        options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "temp_Bongo")
            .Options;
    }

    [Test]
    [Order(1)]
    public void Should_SaveBookingOne()
    {
        //arrange
        using var context = new ApplicationDbContext(options);
        var repo = new StudyRoomBookingRepository(context);

        //act
        repo.Book(studyRoomBookingOne);

        //assert
        var actual = context.StudyRoomBookings.FirstOrDefault(b => b.BookingId == 11);

        //fluent assertion obj comparator
        actual.Should().BeEquivalentTo(studyRoomBookingOne);
    }

    [Test]
    [Order(2)]
    public void Should_ReturnAllBookings()
    {
        //arrange
        var expected = new List<StudyRoomBooking>() { studyRoomBookingOne, studyRoomBookingTwo };

        using var context = new ApplicationDbContext(options);
        context.Database.EnsureDeleted();
        var repo = new StudyRoomBookingRepository(context);

        //act
        repo.Book(studyRoomBookingOne);
        repo.Book(studyRoomBookingTwo);
        var actual = context.StudyRoomBookings.ToList();

        //assert
        actual.Should().BeEquivalentTo(expected);
    }

}