using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.EmailServices
{
    public class EmailSender : IEmailSender
    {
        public EmailConfiguration EmailConfig { get; set; }

        public EmailSender(EmailConfiguration _emailConfig)
        {
            this.EmailConfig = _emailConfig;
        }

        public void SendEmail(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            Send(emailMessage);
        }

        public async Task SendEmailAsync(Message message)
        {
            var mailMessage = CreateEmailMessage(message);

            await SendAsync(mailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(this.EmailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = message.Content };

            if (message.Attachments != null && message.Attachments.Any())
            {
                byte[] fileBytes;
                foreach (var attachment in message.Attachments)
                {
                    using (var ms = new MemoryStream())
                    {
                        attachment.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }

                    bodyBuilder.Attachments.Add(attachment.FileName, fileBytes, ContentType.Parse(attachment.ContentType));
                }
            }

            emailMessage.Body = bodyBuilder.ToMessageBody();
            //emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<h2 style='color:red;'>{0}</h2>", message.Content) };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    if (this.EmailConfig.IsEnabled)
                    {
                        client.Connect(this.EmailConfig.SmtpServer, this.EmailConfig.Port, SecureSocketOptions.Auto);
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        client.Authenticate(this.EmailConfig.UserName, this.EmailConfig.Password);

                        client.Send(mailMessage);
                    }
                }
                catch (Exception ex)
                {
                    client.Disconnect(true);
                    client.Dispose();

                    throw ex;
                }

                client.Disconnect(true);
                client.Dispose();
            }
        }

        private async Task SendAsync(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    if (this.EmailConfig.IsEnabled)
                    {
                        await client.ConnectAsync(this.EmailConfig.SmtpServer, this.EmailConfig.Port, true);
                        client.AuthenticationMechanisms.Remove("XOAUTH2");
                        await client.AuthenticateAsync(this.EmailConfig.UserName, this.EmailConfig.Password);

                        await client.SendAsync(mailMessage);
                    }
                }
                catch (Exception ex)
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();

                    //log an error message or throw an exception, or both.
                    throw ex;
                }

                await client.DisconnectAsync(true);
                client.Dispose();
            }
        }
    }
}