using Microsoft.AspNetCore.Mvc;

namespace MedicalQuestions.Controllers
{
    public class QuestionController : BaseController
    {
        public IActionResult Index()
        {
            this.AttachUserDataToViewBag();

            return View();
        }
    }
}
