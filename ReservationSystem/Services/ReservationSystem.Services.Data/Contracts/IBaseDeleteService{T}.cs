namespace ReservationSystem.Services.Data.Contracts
{
  /// <summary>
  /// Common Delete service.
  /// </summary>
  /// <typeparam name="T">Type of entity.</typeparam>
  public interface IBaseDeleteService<T>
      where T : class
  {
    /// <summary>
    /// Delete <"T"> entity. Not permanent.
    /// </summary>
    /// <param name="entity"><"T"> to be deleted (Not permanent).</param>
    void Delete(T entity);
  }
}
