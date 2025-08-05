using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Infrastructure.Services.Cloudinary
{
    public record CloudinaryUploadResult
    {
        public string Url { get; init; } = null!;
        public string PublicId { get; init; } = null!;
    }
}
