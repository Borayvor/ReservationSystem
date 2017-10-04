using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReservationSystem.Common;
using ReservationSystem.Extensions;
using ReservationSystem.Services.Data.Contracts;
using ReservationSystem.ViewComponents;
using ReservationSystem.ViewModels.CalendarViewModels;

namespace ReservationSystem.Controllers
{
  public class CalendarController : Controller
  {
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly IReservationService reservationService;

    public CalendarController(IMapper mapper, IUnitOfWork unitOfWork, IReservationService reservationService)
    {
      this.mapper = mapper;
      this.unitOfWork = unitOfWork;
      this.reservationService = reservationService;
    }

    [HttpGet]
    public IActionResult Index()
    {
      var currentDate = DateTime.UtcNow.Date;

      return this.View(currentDate);
    }

    [HttpPost]
    public IActionResult GetPreviousMonth(long selectedYearMonth)
    {
      var newSelectedYearMonth = new DateTime(selectedYearMonth).ToUniversalTime().Date;
      var selectedYearMonthPrevious = newSelectedYearMonth.Date.AddMonths(-1);

      return this.ViewComponent(typeof(CalendarViewComponent), new { selectedYearMonth = selectedYearMonthPrevious });
    }

    [HttpPost]
    public IActionResult GetNextMonth(long selectedYearMonth)
    {
      var newSelectedYearMonth = new DateTime(selectedYearMonth).ToUniversalTime().Date;
      var selectedYearMonthNext = newSelectedYearMonth.Date.AddMonths(1);

      return this.ViewComponent(typeof(CalendarViewComponent), new { selectedYearMonth = selectedYearMonthNext });
    }

    [HttpPost]
    [Authorize]
    [ValidateAntiForgeryToken]
    public IActionResult ReservateTheDate(DateTime date)
    {
      if (!this.User.Identity.IsAuthenticated || this.User.IsInRole(GlobalConstants.AdministratorRoleName))
      {
        return this.View("Index", date);
      }

      if (date.ToUniversalTime().Date < DateTime.UtcNow.Date)
      {
        return this.View("Index", date);
      }

      var reservation = this.reservationService.GetByDate(date);

      if (!string.IsNullOrWhiteSpace(reservation.OwnerId))
      {
        return this.View("Index", date);
      }

      var userId = this.User.GetUserId();
      reservation.OwnerId = userId;

      this.reservationService.Update(reservation);
      this.unitOfWork.SaveChanges();

      return this.View("Index", date);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GetSelectedDateData(DateTime date)
    {
      var reservation = this.reservationService.GetByDate(date);

      var newModel = this.mapper.Map<SelectedDateDataViewModel>(reservation);

      return this.ViewComponent(typeof(SelectedDateDataViewComponent), new { viewModel = newModel });
    }
  }
}
