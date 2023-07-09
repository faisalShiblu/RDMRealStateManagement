using Microsoft.AspNetCore.Mvc;

namespace RealStateMVCWebApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult LogIn()
        {
            return View();
        }
    }
}
