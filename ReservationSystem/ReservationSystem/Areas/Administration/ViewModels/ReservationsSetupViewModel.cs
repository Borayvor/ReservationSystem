using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ReservationSystem.ViewModels;

namespace ReservationSystem.Areas.Administration.ViewModels
{
  public class ReservationsSetupViewModel
  {
    public IEnumerable<ReservationViewModel> ReservationsAllUpToDate { get; set; }

    public IEnumerable<ReservationAvailableViewModel> ReservationsАvailable { get; set; }

    [DataType(DataType.Currency)]
    public decimal PriceTotalAllUpToDate { get; set; }
  }
}
