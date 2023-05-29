
using EndPoint3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store.Domain.Entities.Users;
using System.Diagnostics;


namespace EndPoint3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager=userManager;
            _signInManager=signInManager;
        }

        public IActionResult Index()
        {
            //string username = "mehdi@gmail.com";
            //string password = "Aa@123456";

            //var user=_userManager.FindByEmailAsync(username).Result;
            //var result =  _userManager.CheckPasswordAsync(user,
            //                password).Result;

            //if (result)
            //{
            //  var a=  _signInManager.SignInAsync(user, isPersistent: false);

            //}
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}