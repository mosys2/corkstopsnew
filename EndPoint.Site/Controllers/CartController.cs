using EndPoint.Site.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Application.Services.Carts;
using Store.Common.ResultDto;
using Store.Domain.Entities.Users;

namespace EndPoint.Site.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartServices _cartServices;
        private readonly CookiesManager cookiemanager;
        private readonly SignInManager<User> _signInManager;
        public CartController(ICartServices cartServices, SignInManager<User> signInManager)
        {
            _cartServices= cartServices;
            cookiemanager=new CookiesManager();
            _signInManager=signInManager;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> Checkout() {
            if (!_signInManager.IsSignedIn(User))
            {
                Response.Redirect("/Authentication/SignIn");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(string productId, int? count)
        {
            var result =await _cartServices.AddToCart(productId, cookiemanager.GetBrowserId(HttpContext), count);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCartList()
        {

            var userId = ClaimUtility.GetUserId(User);
            var result = _cartServices.GetMyCart(cookiemanager.GetBrowserId(HttpContext), userId);
            return Json(new ResultDto{ IsSuccess=true,});
        }
        [HttpPost]
        public async Task<IActionResult> Remove(string Id)
        {
            return Json(await _cartServices.Remove(Id));
        }
        public IActionResult CartViewComponent()
        {
            return ViewComponent("Cart");
        }

        public IActionResult BacketViewComponent()
        {
            return ViewComponent("Backet");
        }
        public IActionResult CartTableViewComponent()
        {
            return ViewComponent("CartTable");
        }
        public IActionResult BillsViewComponent(int cityId)
        {
            return ViewComponent("Bills", cityId);
        }

    }
}
