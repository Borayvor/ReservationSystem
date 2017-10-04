using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.ViewModels.AccountViewModels
{
  public class ForgotPasswordViewModel
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
  }
}
