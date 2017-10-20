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
    private readonly IEfUnitOfWork unitOfWork;
    private readonly IReservationService reservationService;

    public CalendarController(IMapper mapper, IEfUnitOfWork unitOfWork, IReservationService reservationService)
    {
      this.mapper = mapper ?? throw new ArgumentNullException(
        GlobalConstants.AutoMapperRequiredExceptionMessage);
      this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(
        GlobalConstants.UnitOfWorkRequiredExceptionMessage);
      this.reservationService = reservationService ?? throw new ArgumentNullException(
        GlobalConstants.ReservationServiceRequiredExceptionMessage);
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
        return this.View(nameof(CalendarController.Index), date);
      }

      if (date < DateTime.UtcNow.Date)
      {
        return this.View(nameof(CalendarController.Index), DateTime.UtcNow.Date);
      }

      var reservation = this.reservationService.GetActiveByDate(date);

      if (!string.IsNullOrWhiteSpace(reservation.OwnerId))
      {
        return this.View(nameof(CalendarController.Index), date);
      }

      var userId = this.User.GetUserId();
      reservation.OwnerId = userId;

      this.reservationService.Update(reservation);
      this.unitOfWork.SaveChanges();

      return this.View(nameof(CalendarController.Index), date);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult GetSelectedDateData(DateTime date)
    {
      var reservation = this.reservationService.GetActiveByDate(date);

      var newModel = this.mapper.Map<SelectedDateDataViewModel>(reservation);

      return this.ViewComponent(typeof(SelectedDateDataViewComponent), new { viewModel = newModel });
    }
  }
}
