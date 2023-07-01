using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Carts;
using System.Xml.Linq;

namespace EndPoint.Site.ViewComponents
{
    [ViewComponent(Name = "Bills")]
    public class Bills : ViewComponent
    {
        //private readonly ICartServices _cartService;
        //private readonly CookiesManager cookiesManager;
        //private readonly IGetCityForPayService _getCityForPay;
        //private readonly IGetCityService _getCityService;
        //private readonly IGetProvinceService _getProvinceService;

        //public Bills(ICartServices cartService,
        //    IGetCityForPayService getCityForPay,
        //    IGetProvinceService getProvinceService,
        //    IGetCityService getCityService)
        //{
        //    _cartService = cartService;
        //    _getCityForPay = getCityForPay;
        //    _getCityService = getCityService;
        //    _getProvinceService = getProvinceService;
        //    cookiesManager = new CookiesManager();

        //}
        //public IViewComponentResult Invoke(int? cityId)
        //{
        //    if (cityId==null)
        //    {
        //        int provinceid = _getProvinceService.Execute().Data.First().Id;
        //        int defaultcityId = _getCityService.Execute(provinceid).Data.First().Id;
        //        cityId = defaultcityId;
        //    }

        //    int costAllItem = 0;
        //    int costPost = 0;
        //    var userId = ClaimUtility.GetUserId(HttpContext.User);
        //    var carts = _cartService.GetMyCart(cookiesManager.GetBrowserId(HttpContext), userId).Data;
        //    if (carts.productCount>0)
        //    {
        //        costPost = _getCityForPay.Execute(cityId.Value).Data.SingleOrDefault().Cost;
        //        foreach (var item in carts.CartItems)
        //        {
        //            int costitem = item.Count * item.Price;
        //            costAllItem += costitem;
        //        }
        //    }
        //    ViewBag.costAllItem = costAllItem;
        //    ViewBag.costPost = costPost;
        //    ViewBag.costAll = costAllItem + costPost;
        //    return View(viewName: "Bills");
        //}

    }
}
