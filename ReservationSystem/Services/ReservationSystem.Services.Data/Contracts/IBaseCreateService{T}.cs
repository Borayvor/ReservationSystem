namespace ReservationSystem.Services.Data.Contracts
{
  /// <summary>
  /// Common Create service.
  /// </summary>
  /// <typeparam name="T">Type of entity.</typeparam>
  public interface IBaseCreateService<T>
      where T : class
  {
    /// <summary>
    /// Create new <"T"> entity.
    /// </summary>
    /// <param name="entity"><"T"> to be created.</param>
    void Create(T entity);
  }
}
