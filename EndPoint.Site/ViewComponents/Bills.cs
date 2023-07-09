using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Interfaces.FacadPatternSite;
using Store.Application.Services.Carts;
using Store.Application.Services.Posts.Queries;
using System.Xml.Linq;

namespace EndPoint.Site.ViewComponents
{
    [ViewComponent(Name = "Bills")]
    public class Bills : ViewComponent
    {
        private readonly ICartServices _cartService;
        private readonly CookiesManager cookiesManager;
        private readonly IPostFacadSite _postFacadSite;

        public Bills(ICartServices cartService, IPostFacadSite postFacadSite)
        {
            _cartService = cartService;
            _postFacadSite=postFacadSite;
            cookiesManager = new CookiesManager();

        }
        public IViewComponentResult Invoke(string? cityId)
        {
            double costAllItem = 0;
            double costPost = 0;
            if (cityId==null)
            {
                string provinceid = _postFacadSite.GetProvinceServices.Execute().Result.Data.First().Id;
                string defaultcityId = _postFacadSite.GetCityForPayServices.Execute(provinceid).Result.Data.First().Id;
                cityId = defaultcityId;
            }
            var userId = ClaimUtility.GetUserId(HttpContext.User);
            var carts = _cartService.GetMyCart(cookiesManager.GetBrowserId(HttpContext), userId).Result.Data;
            if (carts.productCount>0)
            {
                costPost = _postFacadSite.GetCityForPayServices.Execute(cityId).Result.Data.SingleOrDefault().Cost;
                foreach (var item in carts.CartItems)
                {
                    double costitem = item.Count * item.Price;
                    costAllItem += costitem;
                }
            }
            ViewBag.carts=carts.CartItems;
            ViewBag.costAllItem = costAllItem.ToString("n0");
            ViewBag.costPost = costPost.ToString("n0");
            ViewBag.costAll = (costAllItem + costPost).ToString("n0");
            return View(viewName: "Bills");
        }

    }
}
