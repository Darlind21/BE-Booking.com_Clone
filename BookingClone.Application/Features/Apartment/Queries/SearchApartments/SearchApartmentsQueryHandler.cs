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

namespace BookingClone.Application.Features.Apartment.Queries.SearchApartments
{
    public class SearchApartmentsQueryHandler
        (IPaginationHelper paginationHelper, IApartmentRepository apartmentRepository)
        : IRequestHandler<SearchApartmentsQuery, Result<PagedList<ApartmentResponseDTO>>>
    {
        public async Task<Result<PagedList<ApartmentResponseDTO>>> Handle(SearchApartmentsQuery request, CancellationToken cancellationToken)
        {
            var query = apartmentRepository.Search(request.SearchParams);

            var projected = query.Select(apartment => new ApartmentResponseDTO
            {
                ApartmentId = apartment.Id,
                Name = apartment.Name,
                Address = apartment.Address,
                PricePerDay = apartment.PricePerDay,
                Description = apartment.Description,
                CleaningFee = apartment.CleaningFee,

                Amenities = apartment.Amenities.Select(a => a.Name).ToList(),

                MainPhoto = apartment.ApartmentPhotos
                    .Where(p => p.IsMainPhoto)
                    .Select(p => new PhotoResponseDTO
                    {
                        ApartmentPhotoId = p.Id,
                        Url = p.Url,
                        PublicId = p.PublicId
                    })
                    .FirstOrDefault() ?? null,

                AverageRating = apartment.Bookings.Any(b => b.Review != null)
                                ? apartment.Bookings.Where(b => b.Review != null).Average(b => (decimal) b.Review!.RatingOutOfTen)
                                : 0m
            });

            return await paginationHelper.PaginateAsync(projected, request.SearchParams.PageNumber, request.SearchParams.PageSize);
        }
    }
}
