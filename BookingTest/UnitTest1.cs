using bookinghelper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    //Mock<>
    public class Tests
    {
        Mock<IBookingRepository> _repository;

        private Booking _existingBooking; 

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IBookingRepository>();
            _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "a"
            };
            var list = new List<Booking>
            {
                _existingBooking
            };

            _repository.Setup(r => r.GetActiveBookings(It.IsAny<int>())).Returns(list.AsQueryable());
           
        }
        



        [Test]
        public void ArrBeforeDepBefore()
        {
            var NewBooking = new Booking
            {
                Id = 2,
                ArrivalDate = Before(_existingBooking.ArrivalDate,-10),
                DepartureDate = Before(_existingBooking.ArrivalDate,-5),
                Reference = "a"
            };
            var result = BookingHelper.OverlappingBookingsExist(NewBooking, _repository.Object);
            Assert.AreEqual(string.Empty, result);
                }
        [Test]
        public void ArrBeforeDepIn()
        {
            var NewBooking = new Booking
            {
                Id = 2,
                ArrivalDate = Before(_existingBooking.ArrivalDate, -10),
                DepartureDate = After(_existingBooking.DepartureDate, -2),
                Reference = "a"
            };
            var result = BookingHelper.OverlappingBookingsExist(NewBooking, _repository.Object);
            Assert.AreEqual(_existingBooking.Reference, result);
        }
        [Test]
        public void ArrInDepIn()
        {
            var NewBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1,16),
                DepartureDate = DepartOn(2017, 1, 19),
                Reference = "a"
            };
            var result = BookingHelper.OverlappingBookingsExist(NewBooking, _repository.Object);
            Assert.AreEqual(_existingBooking.Reference, result);
        }
        [Test]
        public void ArrInDepAfter()
        {
            var NewBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 19),
                DepartureDate = DepartOn(2017, 1, 21),
                Reference = "a"
            };
            var result = BookingHelper.OverlappingBookingsExist(NewBooking, _repository.Object);
            Assert.AreEqual(_existingBooking.Reference, result);
        }
        [Test]
        public void ArrBeforeDepAfter()
        {
            var NewBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 10),
                DepartureDate = DepartOn(2017, 1, 22),
                Reference = "a"
            };
            var result = BookingHelper.OverlappingBookingsExist(NewBooking, _repository.Object);
            Assert.AreEqual(_existingBooking.Reference, result);
        }
        [Test]
        public void ArrAfterDepAfter()
        {
            var NewBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 21),
                DepartureDate = DepartOn(2017, 1, 26),
                Reference = "a"
            };
            var result = BookingHelper.OverlappingBookingsExist(NewBooking, _repository.Object);
            Assert.AreEqual(string.Empty, result);
        }
        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }
        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }
        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }
        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }

    }
}