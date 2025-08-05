using BookingClone.API.Extensions;
using BookingClone.Application.Features.Apartment.Commands.ListNewApartment;
using BookingClone.Application.Features.Apartment.Commands.Photos.AddApartmentPhoto;
using BookingClone.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.API.Controllers
{
    public class ApartmentController(ISender _sender) : BaseAPIController
    {
        [Authorize(Roles = nameof(AppRole.Owner))]
        [HttpPost("list-new-apartment")]
        public async Task<IActionResult> ListNewApartment([FromForm] ListNewApartmentDTO listNewApartmentDTO)
        {
            listNewApartmentDTO = listNewApartmentDTO with { UserId = User.GetUserId() };
            //with keyword creates a copy of the dto and allows us to change some properties while doing so 
            //then we assign the new copy back to the dto 
            var command = new ListNewApartmentCommand { ListNewApartmentDTO = listNewApartmentDTO };

            var result = await _sender.Send(command);

            return result.ToIActionResult();
        }


        [Authorize(Roles = nameof(AppRole.Owner))]
        [HttpPost("add-photos")]
        public async Task<IActionResult> AddApartmentPhotos([FromForm] AddPhotoDTO addPhotoDTO)
        {
            var command = new AddApartmentPhotoCommand { AddPhotoDTO = addPhotoDTO };

            var result = await _sender.Send(command);

            return result.ToIActionResult();
        }
    }
}
