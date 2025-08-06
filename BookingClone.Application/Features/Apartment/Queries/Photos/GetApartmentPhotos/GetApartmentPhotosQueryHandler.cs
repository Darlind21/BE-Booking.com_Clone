using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Interfaces.Repositories;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Apartment.Queries.Photos.GetApartmentPhotos
{
    public class GetApartmentPhotosQueryHandler 
        (IApartmentPhotoRepository photoRepository)
        : IRequestHandler<GetApartmentPhotosQuery, Result<List<PhotoResponseDTO>>>
    {
        public async Task<Result<List<PhotoResponseDTO>>> Handle(GetApartmentPhotosQuery request, CancellationToken cancellationToken)
        {
            var photos = await photoRepository.GetPhotosByApartmentId(request.ApartmentId);

            if (photos.Count() == 0) throw new Exception("No photos found for apartment with this id");

            if (photos.Count() < 4) throw new Exception("Unable to find at least 4 photos for an apartment with this id");


            var response = new List<PhotoResponseDTO>(); 
            foreach(var photo in photos)
            {
                response.Add(new PhotoResponseDTO
                {
                    ApartmentPhotoId = photo.Id,
                    Url = photo.Url,
                    PublicId = photo.PublicId
                });
            }

            return response;
        }
    }
}
