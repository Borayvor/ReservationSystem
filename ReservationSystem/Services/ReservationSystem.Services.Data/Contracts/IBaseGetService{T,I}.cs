using System.Linq;

namespace ReservationSystem.Services.Data.Contracts
{
  /// <summary>
  /// Common Get service.
  /// </summary>
  /// <typeparam name="T">Type of entity.</typeparam>
  /// <typeparam name="I">Type of entity Db Key. Must be "struct" or string.</typeparam>
  public interface IBaseGetService<T, I>
      where T : class
  {
    /// <summary>
    /// Get all <"T"> entities.
    /// </summary>
    /// <returns> Return <"T"> as IQueryable. </returns>
    IQueryable<T> GetAll();

    /// <summary>
    /// Gets the <"T"> entity by id.
    /// </summary>
    /// <param name="id">The id of the <"T"> as <"I"> to get.</param>
    /// <returns>Return <"T"> with id <"I"></returns>
    T GetById(I id);
  }
}
