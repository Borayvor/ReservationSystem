using Microsoft.EntityFrameworkCore;
using ReservationSystem.Services.Data.Contracts;

namespace ReservationSystem.Services.Data
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly DbContext context;

    public UnitOfWork(DbContext context)
    {
      this.context = context;
    }

    public void SaveChanges()
    {
      this.context.SaveChanges();
    }

    public void SaveChangesAsync()
    {
      this.context.SaveChangesAsync();
    }
  }
}
