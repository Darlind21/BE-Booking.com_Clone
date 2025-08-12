using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Common.Helpers;
using BookingClone.Application.Common.Interfaces;
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
        (IBookingRepository bookingRepository, IPaginationHelper paginationHelper)
        : IRequestHandler<GetBookingsForApartmentQuery, Result<PagedList<BookingResponseDTO>>>
    {
        public async Task<Result<PagedList<BookingResponseDTO>>> Handle(GetBookingsForApartmentQuery request, CancellationToken cancellationToken)
        {
            if (request.BookingSearchParams.ApartmentId == null || request.BookingSearchParams.ApartmentId == default)
                throw new Exception("Apartment id not provided to get bookings");

            var query = bookingRepository.GetBookingsByApartmentId(request.BookingSearchParams);

            var projected = query.Select(booking => new BookingResponseDTO
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

            return await paginationHelper.PaginateAsync(projected, request.BookingSearchParams.PageNumber, request.BookingSearchParams.PageSize);
        }
    }
}
