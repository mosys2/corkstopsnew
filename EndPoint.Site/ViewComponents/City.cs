using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Posts.Queries;
using System.Xml.Linq;
using Store.Application.Interfaces.FacadPatternSite;

namespace EndPoint.Site.ViewComponents
{
    [ViewComponent(Name = "City")]
    public class City : ViewComponent
    {
        private readonly IPostFacadSite _postFacadSite;
        public City(IPostFacadSite postFacadSite)
        {
            _postFacadSite= postFacadSite;
        }

        public IViewComponentResult Invoke(string provinceId)
        {
            ViewBag.city = new SelectList(_postFacadSite.GetCityService.Execute(provinceId).Data, "Id", "Name");
            return View(viewName: "City");
        }
    }
}
