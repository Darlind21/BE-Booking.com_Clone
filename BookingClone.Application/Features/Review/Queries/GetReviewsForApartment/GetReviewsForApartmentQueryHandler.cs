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

namespace BookingClone.Application.Features.Review.Queries.GetReviewsForApartment
{
    public class GetReviewsForApartmentQueryHandler
        (IReviewRepository reviewRepository, IApartmentRepository apartmentRepository, IPaginationHelper paginationHelper)
        : IRequestHandler<GetReviewsForApartmentQuery, Result<PagedList<ReviewDTO>>>
    {
        public async Task<Result<PagedList<ReviewDTO>>> Handle(GetReviewsForApartmentQuery request, CancellationToken cancellationToken)
        {
            if (!await apartmentRepository.ApartmentBelongsToOwner(request.ReviewsSearchParams.ApartmentId, request.UserId))
                throw new Exception("Apartment with this id does not belong to owner with this user id");

            var query = reviewRepository.GetReviewsForApartment(request.ReviewsSearchParams);

            var projected = query.Select(r => new ReviewDTO
            {
                RatingOutOfTen = r.RatingOutOfTen,
                Comment = r.Comment,
                BookingId = r.BookingId,
                ReviewId = r.Id
            });

            return await paginationHelper.PaginateAsync(projected, request.ReviewsSearchParams.PageNumber, request.ReviewsSearchParams.PageSize);
        }
    }
}
