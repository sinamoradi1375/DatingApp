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
    }
}