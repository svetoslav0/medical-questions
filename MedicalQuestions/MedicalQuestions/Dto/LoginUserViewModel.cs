using System.ComponentModel.DataAnnotations;

namespace MedicalQuestions.Dto
{
    public class LoginUserViewModel
    {
        [Required]
        [Display(Name = "Потребителско име")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Парола")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
