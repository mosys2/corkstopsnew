namespace EndPoint.Site.Areas.Admin.Models.ContollerModels.Product
{
    public class ProductModelsClass
    {

        public string Name { get; set; }
        public string CategoryId { get; set; }
        public string? BrandId { get; set; }
        public string UserId { get; set; }
        public string? Content { get; set; }
        public double Price { get; set; }
        public double LastPrice { get; set; }
        public int Quantity { get; set; }
        public double PostageFee { get; set; }
        public double PostageFeeBasedQuantity { get; set; }
        public string? Slug { get; set; }
        public bool IsActive { get; set; }
        public string? Pic { get; set; }
        public string? NameTag { get; set; }
        public string? MinPic { get; set; }
        public string[]? Media { get; set; }
        public string[]? TagsId { get; set; }
        public List<FeatureListDto>? FeatureList { get; set; }
    }
    public class FeatureListDto
    {
        public string? Title { get; set; }
        public string? Value { get; set; }
    }
}

