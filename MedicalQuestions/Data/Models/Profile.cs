namespace MedicalQuestions.Data.Models
{
    public class Profile
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string EmailAddress { get; set; }

        public string PhysicalAddress { get; set; }

        public string ContactPerson { get; set; }

        public string Phone { get; set; }

        public string EconomicSector { get; set; }

        public int EmployeesCount { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}