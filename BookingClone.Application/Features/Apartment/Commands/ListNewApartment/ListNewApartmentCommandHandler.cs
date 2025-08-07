using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Application.Interfaces.Services;
using BookingClone.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Commands.ListNewApartment
{
    public class ListNewApartmentCommandHandler
        (IApartmentRepository apartmentRepository, ICloudinaryService cloudinaryService, IOwnerRepository ownerRepository)
        : IRequestHandler<ListNewApartmentCommand, Result<ApartmentResponseDTO>>
    {
        public async Task<Result<ApartmentResponseDTO>> Handle(ListNewApartmentCommand request, CancellationToken cancellationToken)
        {
            var dto = request.ListNewApartmentDTO;

            var existingApartmentSameName = await apartmentRepository
                .ExistsAsync(a => a.Name.Trim().ToLower() == dto.Name.Trim().ToLower());
            if (existingApartmentSameName) return Result.Fail("An apartment with this name exists!");

            var owner = await ownerRepository.GetOwnerByUserIdAsync(dto.UserId);
            if (owner == null) return Result.Fail("Owner with this user id does not exist to lsit new apartment");

            var newApartment = new Domain.Entities.Apartment(dto.Name, dto.Address, dto.PricePerDay, dto.Description, dto.CleaningFee);
            newApartment.Owners.Add(owner);

            foreach (var file in dto.ApartmentPhotos)
            {
                var uploadresult = await cloudinaryService.UploadPhotoAsync(file);
                if (uploadresult.IsFailed) return Result.Fail("Failed to upload photo to cloud");

                var photo = new ApartmentPhoto(uploadresult.Value.Url, uploadresult.Value.PublicId, newApartment.Id);

                newApartment.ApartmentPhotos.Add(photo);
            }

            var mainPhoto = newApartment.ApartmentPhotos.First();
            mainPhoto.SetMainPhoto();//setting the first photo as default main photo

            foreach (var amenity in dto.Amenities) newApartment.Amenities.Add( new Amenity (amenity)); //it is allowed if the owner does not want to add any amenities

            var added = await apartmentRepository.AddAsync(newApartment);
            if (!added) throw new Exception("Unable to add new apartment listing");

            return new ApartmentResponseDTO
            {
                ApartmentId = newApartment.Id,
                Name = newApartment.Name,
                Address = newApartment.Address,
                PricePerDay = newApartment.PricePerDay,
                Description = newApartment.Description,
                CleaningFee = newApartment.CleaningFee,
                Amenities = dto.Amenities,
                MainPhoto = new Common.DTOs.PhotoResponseDTO
                {
                    ApartmentPhotoId =  mainPhoto.Id,
                    Url = mainPhoto.Url,
                    PublicId = mainPhoto.PublicId
                }
            };
        }
    }
}
