using System.Threading.Tasks;

namespace Igmite.Lighthouse.EmailServices
{
    public interface IEmailSender
    {
        void SendEmail(Message message);

        EmailConfiguration EmailConfig { get; set; }

        Task SendEmailAsync(Message message);
    }
}