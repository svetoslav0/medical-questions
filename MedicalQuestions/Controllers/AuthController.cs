using MedicalQuestions.Data;
using MedicalQuestions.Dto;
using MedicalQuestions.Data.Models;

using Microsoft.AspNetCore.Mvc;
using MedicalQuestions.Helpers;
using Microsoft.EntityFrameworkCore;
using MedicalQuestions.Services;

namespace MedicalQuestions.Controllers
{
    public class AuthController : BaseController
    {
        public MladostPublicContext DbContext { get; set; }
        public AuthService AuthService { get; set; }

        public AuthController(
            MladostPublicContext dbContext,
            AuthService authService)
        {
            this.DbContext = dbContext;
            this.AuthService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            string username = this.HttpContext.Session.Get<string>("username");
            if (!string.IsNullOrEmpty(username))
            {
                return this.RedirectToHomePage();
            }

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserViewModel userModel)
        {
            User user = this.DbContext
                .Users
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Username == userModel.Username);

            bool areCredentialsValid = this.AuthService.AreCredentialsValid(user, userModel.Password);

            if (areCredentialsValid)
            {
                this.HttpContext.Session.Set<string>("username", user.Username);
                this.HttpContext.Session.Set<string>("userRole", user.Role.Name);

                return this.RedirectToHomePage();
            }

            ViewBag.ShowError = true;
            return View();
        }

        public IActionResult Logout()
        {
            this.HttpContext.Session.Remove("username");
            this.HttpContext.Session.Remove("userRole");

            return this.RedirectToHomePage();
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

            DbContext.Users.Add(dbUser);
            DbContext.SaveChanges();

            return Ok();
        }
    }
}
