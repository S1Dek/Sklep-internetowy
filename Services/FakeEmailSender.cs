using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Sklep_Internetowy.Services
{
    public class FakeEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Task.CompletedTask; //Symulacja zakończonej operacji
        }
    }
}
