using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Carts;
using System.Xml.Linq;

namespace EndPoint.Site.ViewComponents
{
    [ViewComponent(Name = "Backet")]
    public class Backet : ViewComponent
    {
        private readonly ICartServices _cartService;
        private readonly CookiesManager cookiesManager;
        public Backet(ICartServices cartService)
        {
            _cartService = cartService;
            cookiesManager = new CookiesManager();
        }
        public IViewComponentResult Invoke()
        {
            var userId = ClaimUtility.GetUserId(HttpContext.User);
            return View(viewName: "Backet", _cartService.CartCount(cookiesManager.GetBrowserId(HttpContext), userId).Result);
        }
    }
}
