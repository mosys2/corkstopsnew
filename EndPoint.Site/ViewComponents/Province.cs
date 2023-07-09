using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Posts.Queries;
using System.Xml.Linq;
using Store.Application.Interfaces.FacadPatternSite;

namespace EndPoint.Site.ViewComponents
{
    [ViewComponent(Name = "Province")]
    public class Province : ViewComponent
    {
        private readonly IPostFacadSite _postFacadSite;
        public Province(IPostFacadSite postFacadSite)
        {
           _postFacadSite= postFacadSite;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.province = new SelectList(_postFacadSite.GetProvinceServices.Execute().Result.Data, "Id", "Name");
            return View(viewName: "Province");
        }
    }
}
