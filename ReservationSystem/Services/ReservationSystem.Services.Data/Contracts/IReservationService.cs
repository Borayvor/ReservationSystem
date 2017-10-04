using System;
using ReservationSystem.Data.Models;

namespace ReservationSystem.Services.Data.Contracts
{
  public interface IReservationService :
    IReservationGetService<Reservation, Guid>,
    IBaseCreateService<Reservation>,
    IBaseUpdateService<Reservation>,
    IBaseDeleteService<Reservation>
  {
  }
}
