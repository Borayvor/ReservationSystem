using Microsoft.AspNetCore.Builder;

namespace ReservationSystem.Data
{
  public interface IDataInitializer
  {
    void Initialize(IApplicationBuilder app);
  }
}
