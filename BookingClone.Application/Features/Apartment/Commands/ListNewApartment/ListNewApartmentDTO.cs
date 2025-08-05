using BookingClone.Application.Common.DTOs;
using BookingClone.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace BookingClone.Application.Features.Apartment.Commands.ListNewApartment
{
    public record ListNewApartmentDTO
    {
        public Guid UserId { get; set; }

        public string Name { get; init; } = null!;
        public string Address { get; init; } = null!;
        public decimal PricePerDay { get; init; }
        public string Description { get; init; } = null!;
        public decimal CleaningFee { get;init; }

        public List<string> Amenities { get; set; } = new(20); //an apartment can have at maximum 20 amenities

        public List<IFormFile> ApartmentPhotos { get; set; } = [];
    }
}
