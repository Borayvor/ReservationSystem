using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Common;
using ReservationSystem.Data.Common.Repositories;
using ReservationSystem.Data.Models;
using ReservationSystem.Services.Data.Contracts;

namespace ReservationSystem.Services.Data
{
  public class ReservationService : IReservationService
  {
    private readonly IEfDbRepository<Reservation> reservations;

    public ReservationService(IEfDbRepository<Reservation> reservations)
    {
      this.reservations = reservations ??
        throw new ArgumentNullException(
          GlobalConstants.EfDbRepositoryOfReservation_Required_ExceptionMessage);
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
      if (id == Guid.Empty)
      {
        throw new ArgumentException("Id must not be empty Guid !");
      }

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

    public Reservation GetActiveByDate(DateTime date)
    {
      if (date.Date < DateTime.UtcNow.Date)
      {
        throw new ArgumentOutOfRangeException("You can not get reservation before the current date !");
      }

      var result = this.reservations
        .GetAll()
        .Where(x => x.DateOfReservation.Date >= DateTime.UtcNow.Date)
        .FirstOrDefault(x => x.DateOfReservation.Date == date.Date);

      return result;
    }

    public void Create(Reservation entity)
    {
      if (entity == null)
      {
        throw new ArgumentNullException("Entity must not be null !");
      }

      if (entity.DateOfReservation.Date < DateTime.UtcNow.Date)
      {
        throw new ArgumentNullException("Entity DateOfReservation must be valid !");
      }

      this.reservations.Create(entity);
    }

    public void Update(Reservation entity)
    {
      if (entity == null)
      {
        throw new ArgumentNullException("Entity must not be null !");
      }

      this.reservations.Update(entity);
    }

    public void Delete(Reservation entity)
    {
      if (entity == null)
      {
        throw new ArgumentNullException("Entity must not be null !");
      }

      this.reservations.Delete(entity);
    }
  }
}
