using MedicalQuestions.Data;
using MedicalQuestions.Data.Models;

namespace MedicalQuestions.Services
{
    public class AuthService
    {
        public MladostPublicContext DbContext { get; set; }

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
    }
}
