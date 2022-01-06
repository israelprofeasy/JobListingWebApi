using JobWebApi.AppCommons;
using System.Threading.Tasks;

namespace JobWebApi.AppCores.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);

    }
}
