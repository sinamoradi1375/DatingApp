using System;

namespace DatingApp.Api.Dtos
{
    public class PhotosForDetailedDto
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime UploadedDate { get; set; }
        public bool IsMain { get; set; }
    }
}