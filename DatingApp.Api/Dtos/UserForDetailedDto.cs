using System;
using System.Collections.Generic;
using DatingApp.Api.Models;

namespace DatingApp.Api.Dtos
{
    public class UserForDetailedDto
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Gender { get; set; }
        public string KnownAs { get; set; }
        public int Age { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastActive { get; set; }
        public string Introduction { get; set; }
        public string LookingFor { get; set; }
        public string Interests { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotosForDetailedDto> Photos { get; set; }
    }
}