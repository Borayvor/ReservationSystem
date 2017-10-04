using System;
using System.Collections.Generic;
using ReservationSystem.EnumTypes;

namespace ReservationSystem.ViewModels.CalendarViewModels
{
  public class CalendarViewModel
  {
    public IDictionary<DateTime, ReservationOwnerType> DaysForReservation { get; set; }

    public DateTime CurrentDate { get; set; }

    public DateTime SelectedYearMonth { get; set; }
  }
}
