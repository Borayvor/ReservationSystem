﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ReservationSystem.Data.Models
{
  // Add profile data for application users by adding properties to the ApplicationUser class
  public class ApplicationUser : IdentityUser
  {
    public ICollection<Reservation> Reservations { get; set; }
  }
}
