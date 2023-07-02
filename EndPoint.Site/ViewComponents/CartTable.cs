using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Carts;
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
            var userId = ClaimUtility.GetUserId(HttpContext.User);
            return View(viewName: "CartTable", _cartService.GetMyCart(cookiesManager.GetBrowserId(HttpContext), userId).Result.Data);
        }
    }
}
