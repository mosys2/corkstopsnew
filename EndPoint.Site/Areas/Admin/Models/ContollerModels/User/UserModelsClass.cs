using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace EndPoint.Site.Areas.Admin.Models.ContollerModels.User
{
    public class UserModel_Request
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        public int Gender { get; set; }
        public bool LockoutEnabled { get; set; }
        [Required]
        [Remote(action: "CheckUserExistByMobile", controller: "Common")]

        public string Mobile { get; set; }
        [Required]
        [System.ComponentModel.DataAnnotations.EmailAddress]
        [Remote(action: "CheckUserExistByEmail", controller: "Common")]
        public string Email { get; set; }
        [Required]
        [System.ComponentModel.DataAnnotations.MinLength(6)]
        [System.ComponentModel.DataAnnotations.RegularExpression(@"^.*(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$", ErrorMessage = " password contains a combination of uppercase and lowercase letter and Number and @#$%^&+=")]
        public string Password { get; set; }
        public List<string> Role { get; set; }

    }
    public class DelleteUserModel_Request
    {
        public string Id { get; set; }
    }
    public class UserModel_Edit
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        public int Gender { get; set; }
        public bool LockoutEnabled { get; set; }
        [Required]
        [Remote(action: "CheckUserExistByMobile", controller: "Common", AdditionalFields =nameof(Id))]
        public string Mobile { get; set; }
        [Required]
        [System.ComponentModel.DataAnnotations.EmailAddress]
        [Remote(action: "CheckUserExistByEmail", controller: "Common", AdditionalFields = nameof(Id))]
        public string Email { get; set; }
        public List<string> Role { get; set; }

    }
}
