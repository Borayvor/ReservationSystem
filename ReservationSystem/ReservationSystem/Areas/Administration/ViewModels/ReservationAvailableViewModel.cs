using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Common;

namespace ReservationSystem.Areas.Administration.ViewModels
{
  public class ReservationAvailableViewModel
  {
    [HiddenInput(DisplayValue = false)]
    public Guid Id { get; set; }

    [DisplayFormat(DataFormatString = GlobalConstants.DateFormat, ApplyFormatInEditMode = true)]
    public string DateOfReservation { get; set; }
  }
}
