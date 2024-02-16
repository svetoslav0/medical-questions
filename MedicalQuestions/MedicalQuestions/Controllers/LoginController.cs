using MedicalQuestions.Data;
using MedicalQuestions.Dto;
using MedicalQuestions.Data.Models;

using Microsoft.AspNetCore.Mvc;

namespace MedicalQuestions.Controllers
{
    public class LoginController : Controller
    {
        public MladostPublicContext dbContext { get; set; }
        public LoginController(MladostPublicContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserViewModel user)
        {
            User dbUser = this.dbContext.Users.FirstOrDefault(x => x.Username == user.Username);

            var result = BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password);

            return Ok(result);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DoRegister(LoginUserViewModel user)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

            User dbUser = new User()
            {
                Username = user.Username,
                Password = passwordHash
            };

            dbContext.Users.Add(dbUser);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}
