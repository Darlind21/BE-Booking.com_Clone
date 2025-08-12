using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Interfaces.Repositories;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Booking.Queries.GetBookingDetails
{
    public class GetBookingDetailsQueryHandler
        (IApartmentRepository apartmentRepository, IBookingRepository bookingRepository)
        : IRequestHandler<GetBookingDetailsQuery, Result<BookingResponseDTO>>
    {
        public async Task<Result<BookingResponseDTO>> Handle(GetBookingDetailsQuery request, CancellationToken cancellationToken)
        {
            var booking = await bookingRepository.GetByIdAsync(request.BookingId);
            if (booking == null) throw new Exception("Booking with this id does not exist");

            var apartment = await apartmentRepository.GetByIdAsync(booking.ApartmentId);
            if (apartment == null) throw new Exception("Apartment for this booking does not exist");

            return new BookingResponseDTO
            {
                ApartmentName = apartment.Name,
                ApartmentAddress = apartment.Address,
                CheckinDate = booking.StartDate,
                CheckoutDate = booking.EndDate,
                PriceForPeriod = booking.PriceForPeriod,
                CleaningFee = booking.CleaningFee,
                AmenitiesUpCharge = booking.AmenitiesUpCharge,
                TotalPrice = booking.TotalPrice,
                Status = booking.Status.ToString(),
            };
        }
    }
}
