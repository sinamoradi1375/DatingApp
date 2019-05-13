using System;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.Api.Dtos
{
    public class UserForRegisterDto
    {
        [Required(ErrorMessage = "لطفا فیلد مورد نظر را وارد کنید.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "لطفا فیلد مورد نظر را وارد کنید.")]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "رمز عبور باید حداقل 4 و حداکثر 8 کاراکتر باشد.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "لطفا فیلد مورد نظر را وارد کنید.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "لطفا فیلد مورد نظر را وارد کنید.")]
        public string KnownAs { get; set; }

        [Required(ErrorMessage = "لطفا فیلد مورد نظر را وارد کنید.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "لطفا فیلد مورد نظر را وارد کنید.")]
        public string City { get; set; }

        [Required(ErrorMessage = "لطفا فیلد مورد نظر را وارد کنید.")]
        public string Province { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastActive { get; set; }

        public UserForRegisterDto()
        {
            CreatedDate = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}