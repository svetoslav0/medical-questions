using MedicalQuestions.Data;
using MedicalQuestions.Dto;
using MedicalQuestions.Data.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MedicalQuestions.Helpers;
using Microsoft.EntityFrameworkCore;

namespace MedicalQuestions.Controllers
{
    public class AuthController : Controller
    {
        public MladostPublicContext dbContext { get; set; }

        public AuthController(MladostPublicContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserViewModel user)
        {
            User dbUser = this.dbContext
                .Users
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Username == user.Username);

            if (dbUser == null)
            {
                // TODO: unsuccessful login
            }

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(user.Password, dbUser.Password);

            if (!isPasswordCorrect)
            {
                // TODO: unsuccessful login
            }

            this.HttpContext.Session.Set<string>("username", dbUser.Username);
            this.HttpContext.Session.Set<string>("userRole", dbUser.Role.Name);

            return Ok();
        }

        public IActionResult Logout()
        {
            this.HttpContext.Session.Remove("username");
            this.HttpContext.Session.Remove("userRole");

            return Ok();
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
