using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Commands.Photos.DeleteApartmentPhoto
{
    public class DeleteApartmentPhotoCommandValidator : AbstractValidator<DeleteApartmentPhotoCommand>
    {
        public DeleteApartmentPhotoCommandValidator()
        {
            RuleFor(x => x.DeleteApartmentPhotoDTO.PhotoId)
                .NotEmpty().WithMessage("Apartment photo id is required");
        }
    }
}
