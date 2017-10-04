using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Areas.Administration.ViewModels;
using ReservationSystem.Data.Models;
using ReservationSystem.Services.Data.Contracts;
using ReservationSystem.ViewModels;

namespace ReservationSystem.Areas.Administration.Controllers
{
  [Area("Administration")]
  [Authorize(Roles = "Administrator")]
  [AutoValidateAntiforgeryToken]
  public class ReservationController : Controller
  {
    private readonly IMapper mapper;
    private readonly IUnitOfWork unitOfWork;
    private readonly IReservationService reservationService;

    public ReservationController(IMapper mapper, IUnitOfWork unitOfWork, IReservationService reservationService)
    {
      this.mapper = mapper;
      this.unitOfWork = unitOfWork;
      this.reservationService = reservationService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
      var reservationsAllUpToDate = await this.reservationService.GetAllUpToDate().ToListAsync();

      var priceTotalAllUpToDate = reservationsAllUpToDate.Sum(r => r.Price);
      var reservationsАvailable = reservationsAllUpToDate.Where(r => string.IsNullOrWhiteSpace(r.OwnerId));

      var reservationsAllUpToDateMapped = this.mapper.Map<IEnumerable<ReservationViewModel>>(reservationsAllUpToDate);
      var reservationsАvailableMapped = this.mapper.Map<IEnumerable<ReservationAvailableViewModel>>(reservationsАvailable);

      var model = new ReservationsSetupViewModel()
      {
        ReservationsAllUpToDate = reservationsAllUpToDateMapped,
        ReservationsАvailable = reservationsАvailableMapped,
        PriceTotalAllUpToDate = priceTotalAllUpToDate
      };

      return this.View(model);
    }

    [HttpPost]
    public IActionResult AddReservation(ReservationAddViewModel model)
    {
      if (!this.ModelState.IsValid)
      {
        return this.RedirectToAction("Index");
      }

      var reservation = this.reservationService
        .GetAllUpToDate()
        .FirstOrDefault(r => r.DateOfReservation.Date == model.DateOfReservation.Date);

      if (reservation != null)
      {
        return this.RedirectToAction("Index");
      }

      var newBlankReservation = new Reservation
      {
        DateOfReservation = model.DateOfReservation.Date,
        Price = model.Price
      };

      this.reservationService.Create(newBlankReservation);
      this.unitOfWork.SaveChanges();

      return this.RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult RemoveReservation(ReservationRemoveViewModel model)
    {
      if (!this.ModelState.IsValid)
      {
        return this.RedirectToAction("Index");
      }

      var reservation = this.reservationService.GetById(model.ReservationId);

      if (reservation == null)
      {
        return this.RedirectToAction("Index");
      }

      this.reservationService.Delete(reservation);
      this.unitOfWork.SaveChanges();

      return this.RedirectToAction("Index");
    }
  }
}