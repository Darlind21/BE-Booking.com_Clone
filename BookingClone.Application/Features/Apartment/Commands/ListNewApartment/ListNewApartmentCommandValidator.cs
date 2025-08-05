using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Commands.ListNewApartment
{
    public class ListNewApartmentCommandValidator : AbstractValidator<ListNewApartmentCommand>
    {
        public ListNewApartmentCommandValidator()
        {
            RuleFor(x => x.ListNewApartmentDTO.UserId)
                .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.ListNewApartmentDTO.Name)
                .NotEmpty().WithMessage("Apartment name is required.")
                .MaximumLength(100).WithMessage("Apartment name cannot exceed 100 characters.")
                .MinimumLength(2).WithMessage("Apartment name must be at least 2 chars");

            RuleFor(x => x.ListNewApartmentDTO.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.")
                .MinimumLength(2).WithMessage("Addres must be at least 2 chars");


            RuleFor(x => x.ListNewApartmentDTO.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(x => x.ListNewApartmentDTO.PricePerDay)
                .GreaterThan(0).WithMessage("Price per day must be greater than 0.");

            RuleFor(x => x.ListNewApartmentDTO.CleaningFee)
                .GreaterThanOrEqualTo(0).WithMessage("Cleaning fee must be zero or more.");

            RuleFor(x => x.ListNewApartmentDTO.Amenities)
                .NotNull().WithMessage("Amenities list is required.")
                .Must(a => a.Count <= 20).WithMessage("You can specify up to 20 amenities.")
                .ForEach(a => a.NotEmpty().WithMessage("Amenity cannot be empty."));

            RuleFor(x => x.ListNewApartmentDTO.ApartmentPhotos)
                .Must(p => p.Count >= 4).WithMessage("You must upload at least 4 photos to list new apartment.")
                .ForEach(p =>
                    p.Must(file => file.Length > 0).WithMessage("Uploaded photo cannot be empty.")
                );
        }
    }
}
