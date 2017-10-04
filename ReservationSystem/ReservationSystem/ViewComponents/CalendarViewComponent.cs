using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data.Models;
using ReservationSystem.EnumTypes;
using ReservationSystem.Extensions;
using ReservationSystem.Services.Data.Contracts;
using ReservationSystem.ViewModels.CalendarViewModels;

namespace ReservationSystem.ViewComponents
{
  public class CalendarViewComponent : ViewComponent
  {
    private readonly IReservationService reservationService;

    public CalendarViewComponent(IReservationService reservationService)
    {
      this.reservationService = reservationService;
    }

    public async Task<IViewComponentResult> InvokeAsync(DateTime selectedYearMonth)
    {
      var reservationsAllUpToDate = await this.reservationService
        .GetAllUpToDate()
        .ToListAsync();

      var viewModel = new CalendarViewModel()
      {
        DaysForReservation = this.GetReservationDictionary(reservationsAllUpToDate),
        CurrentDate = DateTime.UtcNow.Date,
        SelectedYearMonth = selectedYearMonth.ToUniversalTime().Date
      };

      return this.View(viewModel);
    }

    private IDictionary<DateTime, ReservationOwnerType> GetReservationDictionary(IList<Reservation> reservationsAllUpToDate)
    {
      IDictionary<DateTime, ReservationOwnerType> reservationDictionary = new Dictionary<DateTime, ReservationOwnerType>();

      foreach (var reservation in reservationsAllUpToDate)
      {
        if (!reservationDictionary.ContainsKey(reservation.DateOfReservation))
        {
          if (string.IsNullOrWhiteSpace(reservation.OwnerId))
          {
            reservationDictionary.Add(reservation.DateOfReservation, ReservationOwnerType.Available);
          }
          else if (reservation.OwnerId == this.UserClaimsPrincipal.GetUserId())
          {
            reservationDictionary.Add(reservation.DateOfReservation, ReservationOwnerType.User);
          }
          else if (reservation.OwnerId != this.UserClaimsPrincipal.GetUserId())
          {
            reservationDictionary.Add(reservation.DateOfReservation, ReservationOwnerType.OtherUser);
          }
        }
      }

      return reservationDictionary;
    }
  }
}
