using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
namespace EndPoint.Site.Areas.Admin.Models.ContollerModels.Category
{
    public class CategoryModelClass
    {
        public string? Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? OrginalName { get; set; }
        public string? Category { get; set; }
        public bool IsActive { get; set; }
        public string? Description { get; set; }
    }
}
