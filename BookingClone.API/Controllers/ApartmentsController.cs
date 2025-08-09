using BookingClone.API.Extensions;
using BookingClone.Application.Common.Helpers;
using BookingClone.Application.Features.Apartment.Commands.ListNewApartment;
using BookingClone.Application.Features.Apartment.Commands.Photos.AddApartmentPhoto;
using BookingClone.Application.Features.Apartment.Commands.Photos.DeleteApartmentPhoto;
using BookingClone.Application.Features.Apartment.Queries.GetApartmentDetails;
using BookingClone.Application.Features.Apartment.Queries.Photos.GetApartmentPhotos;
using BookingClone.Application.Features.Apartment.Queries.SearchApartments;
using BookingClone.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingClone.API.Controllers
{
    public class ApartmentsController(ISender _sender) : BaseAPIController
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


        [Authorize(Roles = nameof(AppRole.Owner))]
        [HttpDelete("delete-photo")]
        public async Task<IActionResult> DeleteApartmentPhoto([FromBody] DeleteApartmentPhotoDTO deleteApartmentPhotoDTO)
        {
            var command = new DeleteApartmentPhotoCommand { DeleteApartmentPhotoDTO = deleteApartmentPhotoDTO };

            var result = await _sender.Send(command);

            return result.ToIActionResult();
        }


        [Authorize]
        [HttpGet("get-apartment-photos/{apartmentId}")]
        public async Task<IActionResult> GetApartmentPhotos([FromRoute] Guid apartmentId)
        {
            var query = new GetApartmentPhotosQuery { ApartmentId = apartmentId };

            var result = await _sender.Send(query);

            return result.ToIActionResult();
        }


        [Authorize] //get apt details as a USER endpoint
        [HttpGet("get-apartment-details/{apartmentId}")]
        public async Task<IActionResult> GetApartmentDetailsForUser([FromRoute] Guid apartmentId)
        {
            var query = new GetApartmentDetailsForUserQuery { ApartmentId = apartmentId };

            var result = await _sender.Send(query);

            return result.ToIActionResult();
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> SearchApartments([FromQuery] SearchParams searchParams)
        {
            var query = new SearchApartmentsQuery { SearchParams = searchParams };

            var result = await _sender.Send(query);

            Response.AddPaginationHeader(result.ValueOrDefault);

            return result.ToIActionResult();
        }
    }
}
