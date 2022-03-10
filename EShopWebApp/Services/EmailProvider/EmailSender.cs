using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace EShopWebApp.Services.EmailProvider
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger _logger;
        private AuthMessageSenderOptions Options { get; } // Set with Secret Manager

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
            ILogger<EmailSender> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            if (string.IsNullOrEmpty(Options.SendGridKey))
            {
                throw new Exception("Null SendGridKey");
            }
            await Execute(Options.SendGridKey, subject, message, email);
        }

        public async Task Execute(string apiKey, string subject, string message, string email)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage
            {
                From = new EmailAddress(Options.PrivateEmail, "Password Recovery"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            // Disable click tracking.
            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
            _logger.LogInformation(response.IsSuccessStatusCode
                               ? $"Email to {email} queued successfully!"
                               : $"Failure Email to {email}");
        }
    }
}
