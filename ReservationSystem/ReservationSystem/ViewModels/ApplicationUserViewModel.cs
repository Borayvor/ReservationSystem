using Microsoft.AspNetCore.Mvc;

namespace ReservationSystem.ViewModels
{
  public class ApplicationUserViewModel
  {
    [HiddenInput(DisplayValue = false)]
    public string Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }
  }
}
