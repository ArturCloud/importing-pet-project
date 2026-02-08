using System.Text;
using MailKit.Net.Smtp;
using MimeKit;

namespace DataImportProj.Services.ImportServices
{
    public class EmailService() : IEmailService
    {
        public async Task SendEmailAsync(bool success, string? exMessage = null, string? importResult = null)
        {
            var msg = new MimeMessage();

            msg.From.Add(new MailboxAddress("", "testimportprojectemail@gmail.com"));
            msg.To.Add(new MailboxAddress("", "lans.grimmo@gmail.com"));
            msg.Subject = "Import results";
            msg.Body = new TextPart("html")
            {
                Text = MessageBody(success, exMessage, importResult)
            };

            using (var client = new SmtpClient())
            {
                // Since this is used only for local, we use Gmail SMTP
                await client.ConnectAsync("smtp.gmail.com", 25, false);
                await client.AuthenticateAsync("testimportprojectemail@gmail.com", "xiyl sltf govw mhro");
                await client.SendAsync(msg);
            }
        }

        private string MessageBody(bool success, string? exMessage = null, string? importResult = null)
        {
            var sb = new StringBuilder();

            sb.AppendLine("<html>");
            sb.AppendLine("<body style=\"font-family: Arial,sans-serif;\">");

            sb.AppendLine("<h1>Import Results</h1>");

            if (success)
            {
                sb.AppendLine("<p style=\"color:green;font-size:15px;\">The import was completed successfully</p>");
                sb.AppendLine(importResult);
            }
            else
            {
                sb.AppendLine($"<p style=\"color:red;font-size:15px;\">The import failed with an error</p>");
                sb.AppendLine($"<p>ErrorMessage - {exMessage}</p>");
            }

            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            return sb.ToString();
        }
    }
}
