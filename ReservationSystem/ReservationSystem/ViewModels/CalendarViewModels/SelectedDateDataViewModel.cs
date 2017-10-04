using System;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.ViewModels.CalendarViewModels
{
  public class SelectedDateDataViewModel
  {
    public DateTime? SelectedDate { get; set; }

    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
  }
}
