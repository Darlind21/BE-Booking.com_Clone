using BookingClone.Application.Common.DTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Review.Commands.SubmitReview
{
    public record SubmitReviewCommand : IRequest<Result<ReviewDTO>>
    {
        public ReviewDTO ReviewDTO { get; init; } = default!;
        public Guid UserId { get; init; }
    }
}
