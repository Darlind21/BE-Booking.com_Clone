using BookingClone.Application.Common.DTOs;
using BookingClone.Application.Interfaces.Repositories;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Features.Booking.Commands.CreateBooking
{
    public class CreateBookingCommandHandler 
        (IApartmentRepository apartmentRepository, IBookingRepository bookingRepository, IUserRepository userRepository)
        : IRequestHandler<CreateBookingCommand, Result<BookingResponseDTO>>
    {
        public async Task<Result<BookingResponseDTO>> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var dto = request.CreateBookingDTO;

            var userExists = await userRepository.ExistsAsync(u => u.Id == dto.UserId);
            if (!userExists) throw new Exception("User with this id does not exist");

            var apartment = await apartmentRepository.GetByIdAsync(dto.ApartmentId);
            if (apartment == null) throw new Exception("Apartment with this Id does not exist");

            var areDatesAvb = await apartmentRepository.IsApartmentAvailable(dto.ApartmentId, dto.CheckinDate, dto.CheckoutDate);
            if (!areDatesAvb) return Result.Fail("Selected checkin and checkout dates are not available");

            var totalDays = (dto.CheckoutDate.ToDateTime(TimeOnly.MinValue) - dto.CheckinDate.ToDateTime(TimeOnly.MinValue)).Days;


            var booking = new Domain.Entities.Booking
                (dto.CheckinDate, dto.CheckoutDate, apartment.PricePerDay * totalDays, apartment.CleaningFee, dto.ApartmentId, dto.UserId);

            var added = await bookingRepository.AddAsync(booking);
            if (!added) throw new Exception("Unable to create booking");

            //FEATURE: Add email notifications;

            return new BookingResponseDTO
            {
                ApartmentName = apartment.Name,
                ApartmentAddress = apartment.Address,
                CheckinDate = booking.StartDate,
                CheckoutDate = booking.EndDate,
                PriceForPeriod = booking.PriceForPeriod,
                CleaningFee = booking.CleaningFee,
                AmenitiesUpCharge = booking.AmenitiesUpCharge, 
                TotalPrice = booking.TotalPrice,
                Status = booking.Status,
            };
        }
    }
}
