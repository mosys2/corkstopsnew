using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace EndPoint.Site.Models
{
    public class RegistrUserModel
    {
        [Required]
        public string FullName { get; set; }

        [Microsoft.Build.Framework.Required]
        [Remote(action: "CheckUserExistByMobile", controller: "Common")]
        public string Mobile { get; set; }
        [Required]
        [System.ComponentModel.DataAnnotations.EmailAddress]
        [Remote(action: "CheckUserExistByEmail", controller: "Common")]
        public string Email { get; set; }
        [Required]
        [System.ComponentModel.DataAnnotations.MinLength(6)]
        [System.ComponentModel.DataAnnotations.RegularExpression(@"^.*(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$",ErrorMessage = " password contains a combination of uppercase and lowercase letter and Number and @#$%^&+=")]
        public string Password { get; set; }
    }
    public class LoginUserModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
