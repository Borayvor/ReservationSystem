using AutoMapper;
using ReservationSystem.Areas.Administration.ViewModels;
using ReservationSystem.Data.Models;
using ReservationSystem.ViewModels;
using ReservationSystem.ViewModels.CalendarViewModels;

namespace ReservationSystem.AutoMapper
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      this.CreateMap<ApplicationUser, ApplicationUserViewModel>()
        .ForMember(m => m.Id, opt => opt.MapFrom(src => src.Id))
        .ForMember(m => m.Email, opt => opt.MapFrom(src => src.Email))
        .ForMember(m => m.UserName, opt => opt.MapFrom(src => src.UserName));

      this.CreateMap<ReservationViewModel, Reservation>();
      this.CreateMap<Reservation, ReservationViewModel>();

      this.CreateMap<Reservation, ReservationAvailableViewModel>()
        .ForMember(m => m.DateOfReservation, opt => opt.MapFrom(src => src.DateOfReservation.Date));

      this.CreateMap<Reservation, SelectedDateDataViewModel>()
        .ForMember(m => m.SelectedDate, opt => opt.MapFrom(src => src.DateOfReservation.Date))
        .ForMember(m => m.Price, opt => opt.MapFrom(src => src.Price));
    }
  }
}
