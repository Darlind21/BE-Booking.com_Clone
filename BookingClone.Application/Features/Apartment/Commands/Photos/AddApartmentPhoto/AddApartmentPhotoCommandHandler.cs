using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Application.Interfaces.Services;
using BookingClone.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Commands.Photos.AddApartmentPhoto
{
    public class AddApartmentPhotoCommandHandler
        (ICloudinaryService cloudinaryService, IApartmentPhotoRepository photoRepository)
        : IRequestHandler<AddApartmentPhotoCommand, Result<List<PhotoResponseDTO>>>
    {
        public async Task<Result<List<PhotoResponseDTO>>> Handle(AddApartmentPhotoCommand request, CancellationToken cancellationToken)
        {
            var dto = request.AddPhotoDTO;

            if (!await photoRepository.ExistsAsync(x => x.ApartmentId == dto.ApartmentId))
                return Result.Fail("Unable to add photo since an apartment with this id does not exist");

            var response = new List<PhotoResponseDTO>();

            foreach (var file in dto.ApartmentPhotos)
            {
                var uploadresult = await cloudinaryService.UploadPhotoAsync(file);
                if (uploadresult.IsFailed) return Result.Fail("Failed to upload photo to cloud");

                var photo = new ApartmentPhoto(uploadresult.Value.Url, uploadresult.Value.PublicId, dto.ApartmentId);

                var added = await photoRepository.AddAsync(photo);
                if (!added) throw new Exception("Unable to add new photo/s for apartment");


                response.Add(new PhotoResponseDTO
                {
                    ApartmentPhotoId = photo.Id,
                    Url = uploadresult.Value.Url,
                    PublicId = uploadresult.Value.PublicId
                });
            }

            return response;
        }
    }
}
