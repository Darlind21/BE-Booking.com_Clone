using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Common.Helpers;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Review.Queries.GetUserReviewHistory
{
    public record GetUserReviewHistoryQuery : IRequest<Result<PagedList<ReviewDTO>>>
    {
        public Guid UserId { get; init; }
        public ReviewSearchParams ReviewSearchParams { get; init; } = default!;
    }
}
