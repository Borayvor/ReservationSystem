using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ReservationSystem.Common;
using ReservationSystem.Data.Models;

namespace ReservationSystem.Data
{
  public class DataInitializer : IDataInitializer
  {
    public async void Initialize(IApplicationBuilder app)
    {
      const string UserEmailString = "user@user.com";

      using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        var userManager = serviceScope.ServiceProvider.GetService<UserManager<ApplicationUser>>();
        var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

        context.Database.EnsureCreated();

        if (!context.Roles.Any(r => r.Name == GlobalConstants.AdministratorRoleName))
        {
          // Create the Administartor Role
          await roleManager.CreateAsync(new IdentityRole(GlobalConstants.AdministratorRoleName));

          // Create the default Admin account
          string adminName = "admin@admin.com";
          string adminPassword = "admin123";
          var admin = new ApplicationUser { UserName = adminName, Email = adminName };
          await userManager.CreateAsync(admin, adminPassword);

          // Create random User account          
          string randomUserPassword = "user123";
          var user = new ApplicationUser { UserName = UserEmailString, Email = UserEmailString };
          await userManager.CreateAsync(user, randomUserPassword);

          // Admin account apply the Administrator role
          await userManager.AddToRoleAsync(await userManager.FindByNameAsync(adminName), GlobalConstants.AdministratorRoleName);

          await context.SaveChangesAsync();
        }

        if (!context.Reservations.Any(x => x.DateOfReservation.Date >= DateTime.UtcNow.Date))
        {
          var rndUser = await userManager.FindByNameAsync(UserEmailString);

          var reservations = new Reservation[]
          {
            new Reservation
            {
              DateOfReservation = DateTime.UtcNow.AddDays(5).Date,
              Price = 34.56M,
              Owner = rndUser,
              OwnerId = rndUser.Id
            },
             new Reservation
             {
              DateOfReservation = DateTime.UtcNow.AddDays(6).Date,
              Price = 34.56M
            },
             new Reservation
             {
              DateOfReservation = DateTime.UtcNow.AddDays(7).Date,
              Price = 34.56M
            },
             new Reservation
             {
              DateOfReservation = DateTime.UtcNow.AddDays(8).Date,
              Price = 34.56M,
              Owner = rndUser,
              OwnerId = rndUser.Id
            },
             new Reservation
             {
              DateOfReservation = DateTime.UtcNow.AddDays(9).Date,
              Price = 34.56M,
              Owner = rndUser,
              OwnerId = rndUser.Id
            }
          };

          foreach (Reservation reservation in reservations)
          {
            await context.Reservations.AddAsync(reservation);
          }

          await context.SaveChangesAsync();
        }
      }
    }
  }
}
