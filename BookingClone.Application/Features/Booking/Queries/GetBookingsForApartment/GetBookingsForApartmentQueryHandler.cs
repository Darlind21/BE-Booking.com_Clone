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
        (IBookingRepository bookingRepository, IPaginationHelper paginationHelper, IApartmentRepository apartmentRepository)
        : IRequestHandler<GetBookingsForApartmentQuery, Result<PagedList<BookingResponseDTO>>>
    {
        public async Task<Result<PagedList<BookingResponseDTO>>> Handle(GetBookingsForApartmentQuery request, CancellationToken cancellationToken)
        {
            if (!await apartmentRepository.ApartmentBelongsToOwner(request.BookingSearchParams.ApartmentId!.Value, request.UserId)) 
                throw new Exception("Apartment with this id does not belong to owner with this user id");

            var query = bookingRepository.GetBookingsByApartmentId(request.BookingSearchParams);

            var projected = query.Select(booking => new BookingResponseDTO
            {
                BookingId = booking.Id,
                ApartmentId = booking.ApartmentId,
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
