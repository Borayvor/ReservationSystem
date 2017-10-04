using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Data.Models;

namespace ReservationSystem.Data
{
  public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      base.OnModelCreating(builder);
      //// Customize the ASP.NET Identity model and override the defaults if needed.
      //// For example, you can rename the ASP.NET Identity table names and more.
      //// Add your customizations after calling base.OnModelCreating(builder);

      this.ConfigureReservation(builder);
    }

    private void ConfigureReservation(ModelBuilder builder)
    {
      builder.Entity<Reservation>()
        .HasKey(m => m.Id);

      builder.Entity<Reservation>()
        .HasAlternateKey(m => m.DateOfReservation);

      builder.Entity<Reservation>()
        .HasOne(m => m.Owner)
        .WithMany(m => m.Reservations)
        .HasForeignKey(m => m.OwnerId);
    }
  }
}
