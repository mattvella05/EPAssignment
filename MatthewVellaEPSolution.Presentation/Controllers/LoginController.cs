using Microsoft.AspNetCore.Mvc;
using MatthewVellaEPSolution.Domain;

namespace MatthewVellaEPSolution.Presentation.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            if (user.Username == "matthew" && user.Password == "password")
            {
                HttpContext.Session.SetString("Username", user.Username);
                return RedirectToAction("Index", "Poll");
            }

            ViewBag.Error = "Invalid username or password";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
