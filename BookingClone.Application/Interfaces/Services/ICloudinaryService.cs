using BookingClone.Infrastructure.Services.Cloudinary;
using FluentResults;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Application.Interfaces.Services
{
    public interface ICloudinaryService
    {
        Task<Result<CloudinaryUploadResult>> UploadPhotoAsync(IFormFile file);
        Task<Result> DeletePhotoAsync(string publicId);
    }
}
