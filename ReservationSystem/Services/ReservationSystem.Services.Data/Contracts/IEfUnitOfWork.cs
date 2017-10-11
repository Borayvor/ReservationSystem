namespace ReservationSystem.Services.Data.Contracts
{
  public interface IEfUnitOfWork
  {
    void SaveChanges();

    void SaveChangesAsync();
  }
}