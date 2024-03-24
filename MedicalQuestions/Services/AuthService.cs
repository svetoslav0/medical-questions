using UnidecodeSharpFork;

using MedicalQuestions.Data;
using MedicalQuestions.Data.Models;
using System.Text.RegularExpressions;
using System;

namespace MedicalQuestions.Services
{
    public class AuthService
    {
        public MladostPublicContext DbContext { get; set; }

        private static readonly Random random = new Random();

        public AuthService(MladostPublicContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public bool AreCredentialsValid(User user, string password)
        {
            if (user == null)
            {
                return false;
            }

            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if (!isPasswordCorrect)
            {
                return false;
            }

            return true;
        }

        public string GenerateUniqueUsername(string companyName)
        {
            while (true)
            {
                string username = this.GenerateUsername(companyName);
                User existingUser = this.DbContext
                    .Users
                    .FirstOrDefault(x => x.Username == username);

                if (existingUser == null)
                {
                    return username;
                }
            }
        }

        public string GeneratePassword(int length = 8)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyz0123456789";
            char[] chars = new char[length];

            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(chars);
        }

        private string GenerateUsername(string companyName)
        {

            string normalized = companyName
                .Unidecode()
                .ToLower();

            normalized = Regex.Replace(normalized, @"[^\w\d_]+", "-").Trim('-');
            normalized = Regex.Replace(normalized, @"\s+", "-");

            int randomNumber = random.Next(10, 100);
            return $"{normalized}-{randomNumber}";
        }
    }
}
