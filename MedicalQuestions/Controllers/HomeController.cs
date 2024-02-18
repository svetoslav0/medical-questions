using MedicalQuestions.Data;
using MedicalQuestions.Models;
using MedicalQuestions.Helpers;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;

namespace MedicalQuestions.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            this.AttachUserDataToViewBag();

            return View();
        }

        public IActionResult Privacy()
        {
            this.AttachUserDataToViewBag();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
