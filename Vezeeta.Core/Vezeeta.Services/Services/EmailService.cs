using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Vezeeta.Services.Services
{
    public class EmailService
    {
        static string SendGridApiKey = "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";

        public async Task<HttpStatusCode> SendEmail(string recipientEmail, string password, string doctorName)
        {
            var client = new SendGridClient(SendGridApiKey);

            var from = new EmailAddress("vezeetaproject@gmail.com", "Vezeeta Project");
            var to = new EmailAddress(recipientEmail,doctorName );
            var subject = "Vezeeta Application: You Have been added to the system!";
            var plainTextContent = $"Hello Doctor,\n" +
                $"This Email is to inform you that you have been added into our system." +
                $"\nPlease Note Your Password is {password}";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, null);

            var response = await client.SendEmailAsync(msg);

            return response.StatusCode;
        }
    }
}
