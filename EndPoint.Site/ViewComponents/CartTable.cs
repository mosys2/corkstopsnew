using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Carts;
using Store.Domain.Entities.Carts;
using System.Xml.Linq;

namespace EndPoint.Site.ViewComponents
{
    [ViewComponent(Name = "CartTable")]
    public class CartTable : ViewComponent
    {
        private readonly ICartServices _cartService;
        private readonly CookiesManager cookiesManager;
        public CartTable(ICartServices cartService)
        {
            _cartService = cartService;
            cookiesManager = new CookiesManager();
        }
        public IViewComponentResult Invoke()
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
            ViewBag.CostAllItem = costAllItem.ToString("n0");
            ViewBag.CostPost = costPost.ToString("n0");
            ViewBag.CostAll = (costAllItem + costPost).ToString("n0"); ;

            return View(viewName: "CartTable",carts);
        }
    }
}
