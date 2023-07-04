using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
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
        //private readonly IGetCityForPayServices _getCityForPay;
        //private readonly IGetCityService _getCityService;
        //private readonly IGetProvinceServices _getProvinceService;

        public Bills(ICartServices cartService
            //IGetCityForPayServices getCityForPay,
            //IGetProvinceServices getProvinceService,
            /*IGetCityService getCityService*/)
        {
            _cartService = cartService;
            //_getCityForPay = getCityForPay;
            //_getCityService = getCityService;
            //_getProvinceService = getProvinceService;
            cookiesManager = new CookiesManager();

        }
        public IViewComponentResult Invoke(string? cityId)
        {
            double costAllItem = 0;
            double costPost = 0;
            var userId = ClaimUtility.GetUserId(HttpContext.User);
            var carts = _cartService.GetMyCart(cookiesManager.GetBrowserId(HttpContext), userId).Result.Data;
            if (carts.productCount>0)
            {
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
