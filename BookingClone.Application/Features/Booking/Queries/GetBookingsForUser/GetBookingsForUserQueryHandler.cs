using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Common.Helpers;
using BookingClone.Application.Common.Interfaces;
using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Booking.Queries.GetBookingsForUser
{
    public class GetBookingsForUserQueryHandler 
        (IBookingRepository bookingRepository, IPaginationHelper paginationHelper)
        : IRequestHandler<GetBookingsForUserQuery, Result<PagedList<BookingResponseDTO>>>
    {
        public async Task<Result<PagedList<BookingResponseDTO>>> Handle(GetBookingsForUserQuery request, CancellationToken cancellationToken)
        {
            if (request.BookingSearchParams.UserId == null || request.BookingSearchParams.UserId == default)
                throw new Exception("User id not provided to get bookings");

            var query = bookingRepository.GetBookingsByUserId(request.BookingSearchParams);

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
                Status = booking.Status.ToString(),
            });

            return await paginationHelper.PaginateAsync(projected, request.BookingSearchParams.PageNumber, request.BookingSearchParams.PageSize);
        }
    }
}
