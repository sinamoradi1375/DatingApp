using System;

namespace DatingApp.Api.Models
{
    public class Photo
    {
        public int ID { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime UploadedDate { get; set; }
        public bool IsMain { get; set; }
        public User User { get; set; }
        public int UserID { get; set; }
    }
}