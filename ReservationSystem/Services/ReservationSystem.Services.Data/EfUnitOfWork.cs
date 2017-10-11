using System;
using Microsoft.EntityFrameworkCore;
using ReservationSystem.Services.Data.Contracts;

namespace ReservationSystem.Services.Data
{
  public class EfUnitOfWork : IEfUnitOfWork
  {
    private readonly DbContext context;

    public EfUnitOfWork(DbContext context)
    {
      this.context = context ?? throw new ArgumentNullException("context");
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
