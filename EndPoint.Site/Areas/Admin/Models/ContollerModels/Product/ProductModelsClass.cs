using Microsoft.Build.Framework;
using Store.Application.Services.Products.Commands.AddNewProduct;

namespace EndPoint.Site.Areas.Admin.Models.ContollerModels.Product
{
    public class ProductModelsClass
    {
        [Required]
        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string? BrandId { get; set; }
        public string? Content { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public double PostageFee { get; set; }
        public double PostageFeeBasedQuantity { get; set; }
        [Required]
        public string? Slug { get; set; }
        public bool IsActive { get; set; }
        public string? Pic { get; set; }
        public string? MinPic { get; set; }
        public string[]? Media { get; set; }
        public string[]? TagsId { get; set; }
        public List<FeatureListDto>? FeatureList { get; set; }
    }
    
}

