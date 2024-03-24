using System.ComponentModel.DataAnnotations;

namespace MedicalQuestions.Dto
{
    public class CreateProfileViewModel
    {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string PhisicalAddress { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string ContactPerson { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string EconomicalSector { get; set; }

        [Required]
        public int EmployeesCount { get; set; }
    }
}
