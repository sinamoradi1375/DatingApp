using System;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Api.Dtos
{
    public class PhotosForCreationDto
    {
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public DateTime AddedDate { get; set; }
        public string Description { get; set; }
        public string PublicId { get; set; }

        public PhotosForCreationDto()
        {
            AddedDate = DateTime.Now;
        }
    }
}