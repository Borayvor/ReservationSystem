using System;
using System.Linq;

namespace ReservationSystem.Services.Data.Contracts
{
  /// <summary>
  /// Common Get service.
  /// </summary>
  /// <typeparam name="T">Type of entity.</typeparam>
  /// <typeparam name="I">Type of entity Db Key. Must be "struct" or string.</typeparam>
  public interface IReservationGetService<T, I> : IBaseGetService<T, I>
      where T : class
  {
    /// <summary>
    /// Get all up to date <"T"> entities.
    /// </summary>
    /// <returns> Return <"T"> as IQueryable. </returns>
    IQueryable<T> GetAllUpToDate();

    /// <summary>
    /// Get <"T"> entity.
    /// </summary>
    /// <param name="date">Reservation date.</param>
    /// <returns> Return <"T"> with date <"date">.</returns>
    T GetByDate(DateTime date);

    /// <summary>
    /// Get <"T"> entity.
    /// </summary>
    /// <param name="date">Reservation date.</param>
    /// <returns> Return active <"T"> with date <"date">.</returns>
    T GetActiveByDate(DateTime date);
  }
}
