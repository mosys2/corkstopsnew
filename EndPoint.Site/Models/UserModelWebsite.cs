using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace EndPoint.Site.Models
{
    public class RegistrUserModel
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        [Remote(action: "CheckUserExistByMobile", controller: "Common")]

        public string Mobile { get; set; }
        [Required]
        [Remote(action: "CheckUserExistByEmail", controller: "Common")]
        public string Email { get; set; }
        [Required]
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
