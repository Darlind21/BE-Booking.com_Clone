using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Interfaces.Repositories;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Booking.Queries.GetBookingsForApartment
{
    public class GetBookingsForApartmentQueryHandler
        (IBookingRepository bookingRepository)
        : IRequestHandler<GetBookingsForApartmentQuery, Result<List<BookingResponseDTO>>>
    {
        public async Task<Result<List<BookingResponseDTO>>> Handle(GetBookingsForApartmentQuery request, CancellationToken cancellationToken)
        {
            var bookings = await bookingRepository.GetBookingsByApartmentId(request.ApartmentId);

            var response = new List<BookingResponseDTO>();

            if (bookings.Count == 0) return response;

            foreach (var booking in bookings)
            {
                response.Add(new BookingResponseDTO
                {
                    ApartmentName = booking.Apartment.Name,
                    ApartmentAddress = booking.Apartment.Address,
                    CheckinDate = booking.StartDate,
                    CheckoutDate = booking.EndDate,
                    PriceForPeriod = booking.PriceForPeriod,
                    CleaningFee = booking.CleaningFee,
                    AmenitiesUpCharge = booking.AmenitiesUpCharge,
                    TotalPrice = booking.TotalPrice,
                    Status = booking.Status,
                });
            }

            return response;
        }
    }
}
