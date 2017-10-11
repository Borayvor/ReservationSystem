using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ReservationSystem.Data.Common.Repositories
{
  public class EfDbRepository<T> : IEfDbRepository<T>
    where T : class
  {
    public EfDbRepository(DbContext context)
    {
      this.Context = context;
      this.DbSet = this.Context.Set<T>();
    }

    private DbSet<T> DbSet { get; set; }

    private DbContext Context { get; set; }

    public IQueryable<T> GetAll()
    {
      return this.DbSet;
    }

    public T GetById(object id)
    {
      return this.DbSet.Find(id);
    }

    public void Create(T entity)
    {
      var entry = this.Context.Entry(entity);

      if (entry.State != EntityState.Detached)
      {
        entry.State = EntityState.Added;
      }
      else
      {
        this.DbSet.Add(entity);
      }
    }

    public void Update(T entity)
    {
      var entry = this.Context.Entry(entity);

      if (entry.State == EntityState.Detached)
      {
        this.DbSet.Attach(entity);
      }

      entry.State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
      var entry = this.Context.Entry(entity);

      if (entry.State != EntityState.Deleted)
      {
        entry.State = EntityState.Deleted;
      }
      else
      {
        this.DbSet.Attach(entity);
        this.DbSet.Remove(entity);
      }
    }
  }
}
