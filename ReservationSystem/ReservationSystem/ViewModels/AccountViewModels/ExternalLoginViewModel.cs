using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.ViewModels.AccountViewModels
{
  public class ExternalLoginViewModel
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
  }
}
