using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Carts;

namespace EndPoint.Site.ViewComponents
{
    [ViewComponent(Name = "Cart")]
    public class Cart : ViewComponent
    {
        private readonly ICartServices _cartService;
        private readonly CookiesManager cookiesManager;
        public Cart(ICartServices cartService)
        {
            _cartService = cartService;
            cookiesManager = new CookiesManager();
        }
        public IViewComponentResult Invoke()
        {
            var userId = ClaimUtility.GetUserId(HttpContext.User);
            var cart = _cartService.GetMyCart(cookiesManager.GetBrowserId(HttpContext), userId).Result.Data;
            return View(viewName: "Cart",cart);
        }
    }
}
