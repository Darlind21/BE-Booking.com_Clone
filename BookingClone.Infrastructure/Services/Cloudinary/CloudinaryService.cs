using BookingClone.Application.Interfaces.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Infrastructure.Services.Cloudinary
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly CloudinaryDotNet.Cloudinary _cloudinary;

        public CloudinaryService(IOptions<CloudinarySettings> config) //IOptions is a way of injecting appsettings.json into the code
        {
            var acc = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);

            _cloudinary = new CloudinaryDotNet.Cloudinary(acc);

            _cloudinary.Api.Secure = true; //tells cloudinary to use https
        }


        public async Task<Result> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            if (result.Result == "ok") return Result.Ok();//result of successful deletion is "ok"
            else return Result.Fail("Failed to delete photo from Cloudinary");
        }

        public async Task<Result<CloudinaryUploadResult>> UploadPhotoAsync(IFormFile file)
        {
            if (file is null || file.Length == 0)
                return Result.Fail("File is empty or null.");

            await using var stream = file.OpenReadStream(); //opens the file to get its data as a readable stream(like a flow of bytes)

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Crop("fill").Gravity("auto"),
                Folder = "BookingApp"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode != HttpStatusCode.OK || string.IsNullOrEmpty(uploadResult.SecureUrl?.ToString()))
            {
                return Result.Fail("Photo upload failed.");
            }

            var result = new CloudinaryUploadResult
            {
                Url = uploadResult.SecureUrl.ToString(),
                PublicId = uploadResult.PublicId
            };

            return Result.Ok(result);
        }

    }
}
