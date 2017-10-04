using Microsoft.AspNetCore.Mvc;
using ReservationSystem.ViewModels.CalendarViewModels;

namespace ReservationSystem.ViewComponents
{
  public class SelectedDateDataViewComponent : ViewComponent
  {
    public IViewComponentResult Invoke(SelectedDateDataViewModel viewModel)
    {
      return this.View(viewModel);
    }
  }
}
