namespace MedicalQuestions.Data.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public UserRole Role { get; set; }

        public int RoleId { get; set; }

        public Profile Profile { get; set; }
    }
}
