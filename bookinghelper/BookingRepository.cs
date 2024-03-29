﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bookinghelper
{
    public class BookingRepository : IBookingRepository
    {
        public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
        {
            var unitOfWork = new UnitOfWork();
            return unitOfWork.Query<Booking>()
                .Where(
                        b => b.Id != excludedBookingId && b.Status != "Cancelled");
        }
    }
}
