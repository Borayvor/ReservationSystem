using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Areas.Administration.ViewModels
{
  public class ReservationRemoveViewModel
  {
    public ReservationRemoveViewModel()
    {
    }

    public ReservationRemoveViewModel(IEnumerable<ReservationAvailableViewModel> reservationsАvailable)
    {
      this.ReservationsАvailable = reservationsАvailable;
    }

    public IEnumerable<ReservationAvailableViewModel> ReservationsАvailable { get; set; }

    [Display(Name = "Date of reservation")]
    public Guid ReservationId { get; set; }
  }
}
