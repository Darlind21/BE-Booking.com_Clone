using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Commands.Photos.AddApartmentPhoto
{
    public class AddApartmentPhotoCommandValidator : AbstractValidator<AddApartmentPhotoCommand>
    {
        public AddApartmentPhotoCommandValidator()
        {
            RuleFor(x => x.AddPhotoDTO.ApartmentId)
                .NotEmpty().WithMessage("Apartment id cannot be empty when adding photos");
            RuleFor(x => x.AddPhotoDTO.ApartmentPhotos)
                .NotEmpty().WithMessage("Photos to add cannot be empty");
        }
    }
}
