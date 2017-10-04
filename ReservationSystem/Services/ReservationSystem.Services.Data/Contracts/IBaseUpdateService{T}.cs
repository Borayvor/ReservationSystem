namespace ReservationSystem.Services.Data.Contracts
{
  /// <summary>
  /// Common Update service.
  /// </summary>
  /// <typeparam name="T">Type of entity.</typeparam>
  public interface IBaseUpdateService<T>
      where T : class
  {
    /// <summary>
    /// Update <"T"> entity.
    /// </summary>
    /// <param name="entity"><"T"> to be updated.</param>
    void Update(T entity);
  }
}
