using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Common.Helpers;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Review.Queries.GetReviewsForApartment
{
    public record GetReviewsForApartmentQuery : IRequest<Result<PagedList<ReviewDTO>>>
    {
        public Guid UserId { get; init; }
        public ReviewSearchParams ReviewsSearchParams { get; init; } = default!;
    }
}
