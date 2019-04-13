using System;

namespace DatingApp.Api.Dtos
{
    public class UserForListDto
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string KnownAs { get; set; }
        public int Age { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastActive { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string PhotoUrl { get; set; }
    }
}