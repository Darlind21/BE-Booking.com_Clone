using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Features.Apartment.Queries.GetApartmentDetails;
using BookingClone.Application.Interfaces.Repositories;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Queries.GetApartmentDetailsForUser
{
    public class GetApartmentDetailsForUserQueryHandler 
        (IApartmentRepository apartmentRepository, IApartmentPhotoRepository photoRepository)
        : IRequestHandler<GetApartmentDetailsForUserQuery, Result<ApartmentDetailsForUserDTO>>
    {
        public async Task<Result<ApartmentDetailsForUserDTO>> Handle(GetApartmentDetailsForUserQuery request, CancellationToken cancellationToken)
        {
            var apartment = await apartmentRepository.GetByIdAsync(request.ApartmentId);
            if (apartment == null) throw new Exception("Apartment with this id not found");

            var photos = await photoRepository.GetPhotosByApartmentId(request.ApartmentId);

            if (photos.Count() == 0) throw new Exception("No photos found for apartment with this id");

            if (photos.Count() < 4) throw new Exception("Unable to find at least 4 photos for an apartment with this id");

            var photoList = new List<PhotoResponseDTO>();
            foreach (var photo in photos)
            {
                photoList.Add(new PhotoResponseDTO
                {
                    ApartmentPhotoId = photo.Id,
                    Url = photo.Url,
                    PublicId = photo.PublicId
                });
            }

            var amenities = await apartmentRepository.GetAmenitiesForApartment(request.ApartmentId);

            var amenitiesList = new List<string>();
            foreach (var amenity in amenities)
            {
                amenitiesList.Add(amenity.Name);
            }

            return new ApartmentDetailsForUserDTO
            {
                Name = apartment.Name,
                Address = apartment.Address,
                PricePerDay = apartment.PricePerDay,
                Description = apartment.Description,
                CleaningFee = apartment.CleaningFee,
                ApartmentPhotos = photoList,
                Amenities = amenitiesList,
                AverageRating = await apartmentRepository.GetApartmentAverageRating(apartment.Id)
            };
        }
    }
}
