using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Queries.SearchApartments
{
    public class SearchApartmentsQueryValidator : AbstractValidator<SearchApartmentsQuery>
    {
        public SearchApartmentsQueryValidator()
        {
            RuleFor(x => x.SortBy)
                .IsInEnum()
                .WithMessage("Invalid sort option.");
        }
    }
}
