namespace ReservationSystem.Services.Data.Contracts
{
  public interface IUnitOfWork
  {
    void SaveChanges();

    void SaveChangesAsync();
  }
}