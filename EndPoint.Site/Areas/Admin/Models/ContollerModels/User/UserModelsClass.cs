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
        public int[] Rolls { get; set; }
        public int Gender { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [Remote(action: "CheckUserExistByMobile", controller: "Common")]

        public string Mobile { get; set; }
        [Required]
        [Remote(action: "CheckUserExistByEmail", controller: "Common")]
        public string Email { get; set; }
        [Required]
        [Remote(action: "CheckUserExistByUsername", controller: "Common")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Address { get; set; }
    }
    public class DelleteUserModel_Request
    {
        public long currentItemId { get; set; }
    }
    public class UserModel_Edit
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        public long[] Rolls { get; set; }
        public int Gender { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [Remote(action: "CheckUserExistByMobile", controller: "Common", AdditionalFields = nameof(Id))]
        public string Mobile { get; set; }
        [Required]
        [Remote(action: "CheckUserExistByEmail", controller: "Common", AdditionalFields =nameof(Id))]
        public string Email { get; set; }
        [Required]
        [Remote(action: "CheckUserExistByUsername", controller: "Common", AdditionalFields = nameof(Id))]
        public string Username { get; set; }

        public string? Address { get; set; }
    }
}
