using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data.Common.Repositories;
using ReservationSystem.Data.Models;
using ReservationSystem.Services.Data.Contracts;

namespace ReservationSystem.Services.Data
{
  public class ReservationService : IReservationService
  {
    private readonly IReservationSystemEfRepository<Reservation> reservations;

    public ReservationService(IReservationSystemEfRepository<Reservation> reservations)
    {
      this.reservations = reservations;
    }

    public IQueryable<Reservation> GetAll()
    {
      var result = this.reservations
        .GetAll()
        .Include(x => x.Owner)
        .OrderByDescending(x => x.DateOfReservation);

      return result;
    }

    public IQueryable<Reservation> GetAllUpToDate()
    {
      var result = this.reservations
        .GetAll()
        .Include(x => x.Owner)
        .Where(x => x.DateOfReservation.Date >= DateTime.UtcNow.Date)
        .OrderByDescending(x => x.DateOfReservation);

      return result;
    }

    public Reservation GetById(Guid id)
    {
      var result = this.reservations.GetById(id);

      return result;
    }

    public Reservation GetByDate(DateTime date)
    {
      var result = this.reservations
        .GetAll()
        .FirstOrDefault(x => x.DateOfReservation.Date == date.Date);

      return result;
    }

    public void Create(Reservation entity)
    {
      this.reservations.Create(entity);
    }

    public void Update(Reservation entity)
    {
      this.reservations.Update(entity);
    }

    public void Delete(Reservation entity)
    {
      this.reservations.Delete(entity);
    }
  }
}
