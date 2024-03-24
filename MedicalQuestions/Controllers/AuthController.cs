using MedicalQuestions.Data;
using MedicalQuestions.Dto;
using MedicalQuestions.Data.Models;

using Microsoft.AspNetCore.Mvc;
using MedicalQuestions.Helpers;
using Microsoft.EntityFrameworkCore;
using MedicalQuestions.Services;
using MedicalQuestions.Email;
using MedicalQuestions.Email.Interfaces;

namespace MedicalQuestions.Controllers
{
    public class AuthController : BaseController
    {
        public MladostPublicContext DbContext { get; set; }
        public AuthService AuthService { get; set; }
        public IEmailSender EmailSender { get; set; }

        public AuthController(
            MladostPublicContext dbContext,
            AuthService authService,
            IEmailSender emailSender)
        {
            this.DbContext = dbContext;
            this.AuthService = authService;
            EmailSender = emailSender;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (this.IsUserLoggedIn())
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

        public IActionResult SignUp()
        {
            if (this.IsUserLoggedIn())
            {
                return this.RedirectToHomePage();
            }

            return View();
        }

        [HttpPost]
        public IActionResult DoSignUp(CreateProfileViewModel model)
        {
            string userPassword = this.AuthService.GeneratePassword();

            User user = new User
            {
                Username = this.AuthService.GenerateUniqueUsername(model.CompanyName),
                Password = BCrypt.Net.BCrypt.HashPassword(userPassword),
                RoleId = Common.Constants.UserRole.User
            };

            Profile profile = new Profile
            {
                CompanyName = model.CompanyName,
                PhysicalAddress = model.PhisicalAddress,
                EmailAddress = model.EmailAddress,
                ContactPerson = model.ContactPerson,
                Phone = model.Phone,
                EconomicSector = model.EconomicalSector,
                EmployeesCount = model.EmployeesCount,
                User = user
            };

            user.Profile = profile;

            string mailSubject = "Вашият профил е създаден";
            string mailBody = $"Използвайте потребителското име и парола по-долу за вход в системата. Потребителско име: {user.Username}, парола: {userPassword}";
            var message = new Message(model.EmailAddress, mailSubject, mailBody);
            this.EmailSender.SendEmail(message);

            this.DbContext.Users.Add(user);
            this.DbContext.Profiles.Add(profile);
            this.DbContext.SaveChanges();

            return Ok();
        }


        // todo: will be removed in prod
        public IActionResult Register()
        {
            return View();
        }

        // todo: will be removed in prod
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
