using System;

namespace ReservationSystem.Data.Models
{
  public class Reservation
  {
    public Guid Id { get; set; }

    public DateTime DateOfReservation { get; set; }

    public decimal Price { get; set; }

    public string OwnerId { get; set; }

    public ApplicationUser Owner { get; set; }
  }
}
