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

            _repository.Setup(r => r.GetActiveBookings(2)).Returns(list.AsQueryable());
           
        }
        



        [Test]
        public void test1()
        {
            var NewBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 05),
                DepartureDate = DepartOn(2017, 1, 10),
                Reference = "a"
            };
            var result = BookingHelper.OverlappingBookingsExist(NewBooking, _repository.Object);
            Assert.AreEqual(string.Empty, result);
                }
        [Test]
        public void test2()
        {
            var NewBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 10),
                DepartureDate = DepartOn(2017, 1, 16),
                Reference = "a"
            };
            var result = BookingHelper.OverlappingBookingsExist(NewBooking, _repository.Object);
            Assert.AreEqual("a", result);
        }
        [Test]
        public void test3()
        {
            var NewBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1,16),
                DepartureDate = DepartOn(2017, 1, 19),
                Reference = "a"
            };
            var result = BookingHelper.OverlappingBookingsExist(NewBooking, _repository.Object);
            Assert.AreEqual("a", result);
        }
        [Test]
        public void test4()
        {
            var NewBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 19),
                DepartureDate = DepartOn(2017, 1, 21),
                Reference = "a"
            };
            var result = BookingHelper.OverlappingBookingsExist(NewBooking, _repository.Object);
            Assert.AreEqual("a", result);
        }
        [Test]
        public void test5()
        {
            var NewBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 10),
                DepartureDate = DepartOn(2017, 1, 22),
                Reference = "a"
            };
            var result = BookingHelper.OverlappingBookingsExist(NewBooking, _repository.Object);
            Assert.AreEqual("a", result);
        }
        [Test]
        public void test6()
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