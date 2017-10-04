using System.Linq;

namespace ReservationSystem.Data.Common.Repositories
{
  public interface IReservationSystemEfRepository<T>
    where T : class
  {
    IQueryable<T> GetAll();

    T GetById(object id);

    void Create(T entity);

    void Update(T entity);

    void Delete(T entity);
  }
}
