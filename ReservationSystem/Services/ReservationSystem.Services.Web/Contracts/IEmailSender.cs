using System.Threading.Tasks;

namespace ReservationSystem.Services.Web.Contracts
{
  public interface IEmailSender
  {
    Task SendEmailAsync(string email, string subject, string message);
  }
}
