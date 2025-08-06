using BookingClone.Application.Interfaces.Repositories;
using BookingClone.Application.Interfaces.Services;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Commands.Photos.DeleteApartmentPhoto
{
    public class DeleteApartmentPhotoCommandHandler 
        (IApartmentPhotoRepository photoRepository,
        IApartmentRepository apartmentRepository,
        ICloudinaryService cloudinaryService)
        : IRequestHandler<DeleteApartmentPhotoCommand, Result>
    {
        public async Task<Result> Handle(DeleteApartmentPhotoCommand request, CancellationToken cancellationToken)
        {
            var dto = request.DeleteApartmentPhotoDTO;

            var photoToDelete = await photoRepository.ExistsAsync(ap => ap.Id == dto.PhotoId);
            if (!photoToDelete) throw new Exception("Photo to delete with this id does not exist");

            var aptExists = await apartmentRepository.ExistsAsync(a => a.Id == dto.ApartmentId);
            if (!aptExists) throw new Exception("Apartment with this id does not exist");

            if (await photoRepository.GetPhotosCountForApartment(dto.ApartmentId) <= 4)
                return Result.Fail("Unable to delete photo as an apartment must always have at least 4 photos");

            var cloudinaryDeleted = await cloudinaryService.DeletePhotoAsync(dto.PublicId);
            if (cloudinaryDeleted.IsFailed) throw new Exception("Unable to delete photo from cloudinary");

            var deleted = await photoRepository.DeleteByIdAsync(dto.PhotoId);
            if (!deleted) throw new Exception("Unable to delete apartment photo from database");

            return Result.Ok();
        }
    }
}
