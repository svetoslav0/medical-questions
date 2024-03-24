using MailKit.Net.Smtp;
using MimeKit;

namespace MedicalQuestions.Email
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public Message(string to, string subject, string content)
        {
            To = new List<MailboxAddress>
            {
                new MailboxAddress(string.Empty, to)
            };
            Subject = subject;
            Content = content;
        }
    }
}
