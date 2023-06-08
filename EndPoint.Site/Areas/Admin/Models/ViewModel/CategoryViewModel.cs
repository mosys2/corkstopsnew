using EndPoint.Site.Areas.Admin.Models.ContollerModels.Category;
using Store.Application.Services.Categuries.Queries.GetAllCateguryForSelectList;

namespace EndPoint.Site.Areas.Admin.Models.ViewModel
{
    public class CategoryViewModel
    {
        public List<AllCateguriesSelectListDto> allCateguries { get; set; }
        public CategoryModelClass categoryModel { get; set; }
    }
}
