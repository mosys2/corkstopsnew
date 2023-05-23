using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Controllers
{
    public class AuthenticationController : Controller
    {
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
    }
}
