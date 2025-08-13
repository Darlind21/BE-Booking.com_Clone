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

namespace BookingClone.Application.Features.Review.Queries.GetUserReviewHistory
{
    public class GetUserReviewHistoryQueryHandler 
        (IReviewRepository reviewRepository, IPaginationHelper paginationHelper)
        : IRequestHandler<GetUserReviewHistoryQuery, Result<PagedList<ReviewDTO>>>
    {
        public async Task<Result<PagedList<ReviewDTO>>> Handle(GetUserReviewHistoryQuery request, CancellationToken cancellationToken)
        {
            var query = reviewRepository.GetReviewsForUser(request.ReviewSearchParams, request.UserId);

            var projected = query.Select(r => new ReviewDTO
            {
                RatingOutOfTen = r.RatingOutOfTen,
                Comment = r.Comment,
                BookingId = r.BookingId,
                ReviewId = r.Id
            });

            return await paginationHelper.PaginateAsync(projected, request.ReviewSearchParams.PageNumber, request.ReviewSearchParams.PageSize);
        }
    }
}
