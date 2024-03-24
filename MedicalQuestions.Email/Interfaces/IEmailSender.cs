namespace MedicalQuestions.Email.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(Message message);
    }
}
