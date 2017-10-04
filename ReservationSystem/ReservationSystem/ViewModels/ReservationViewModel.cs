using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Common;

namespace ReservationSystem.ViewModels
{
  public class ReservationViewModel
  {
    [HiddenInput(DisplayValue = false)]
    public Guid Id { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = GlobalConstants.DateFormat, ApplyFormatInEditMode = true)]
    public DateTime DateOfReservation { get; set; }

    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    public ApplicationUserViewModel Owner { get; set; }
  }
}
