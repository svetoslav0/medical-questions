using MedicalQuestions.Helpers;

using Microsoft.AspNetCore.Mvc;

namespace MedicalQuestions.Controllers
{
    public abstract class BaseController : Controller
    {
        protected IActionResult RedirectToHomePage()
        {
            return this.RedirectToAction("Index", "Home");
        }

        protected void AttachUserDataToViewBag()
        {
            string username = this.HttpContext.Session.Get<string>("username");
            string userRole = this.HttpContext.Session.Get<string>("userRole");

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(userRole))
            {
                ViewBag.IsUserLoggedIn = true;
                ViewBag.Username = username;
                ViewBag.UserRole = userRole.ToLower();
            }
        }

        protected bool IsUserLoggedIn()
        {
            string username = this.HttpContext.Session.Get<string>("username");

            return !string.IsNullOrEmpty(username);
        }
    }
}
