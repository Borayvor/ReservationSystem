using System;
using System.ComponentModel.DataAnnotations;
using ReservationSystem.Common;

namespace ReservationSystem.Areas.Administration.ViewModels
{
  public class ReservationAddViewModel
  {
    [Display(Name = "Date for reservation")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = GlobalConstants.DateFormat, ApplyFormatInEditMode = true)]
    public DateTime DateOfReservation { get; set; }

    [DataType(DataType.Currency)]
    [Range(0.00, 1000000000000.00)]
    public decimal Price { get; set; }
  }
}
