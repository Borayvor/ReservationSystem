namespace ReservationSystem.Common
{
  public class GlobalConstants
  {
    public const string AdministratorRoleName = "Administrator";

    public const string CalendarTitle = "Select Date";
    public const string CalendarSelectedDateMessage = "You have selected:";

    public const string DateFormat = "{0:dd-MM-yyyy}";

    //// Exception Messages
    public const string AutoMapperRequiredExceptionMessage = "An instance of AutoMapper is required !";
    public const string EfDbRepositoryOfReservationRequiredExceptionMessage = "An instance of EfDbRepository of Reservation is required !";
    public const string GetByIdGuidEmptyExceptionMessage = "Id must not be empty Guid !";
    public const string ReservationRequiredExceptionMessage = "An instance of Reservation is required !";
    public const string UnitOfWorkRequiredExceptionMessage = "An instance of UnitOfWork is required !";

    //// Exception Messages - ReservationService
    public const string ReservationServiceRequiredExceptionMessage = "An instance of ReservationService is required !";
    public const string ReservationServiceGetActiveByDateActiveDateExceptionMessage = "You can not get reservation before the current date !";
    public const string ReservationServiceCreateActiveDateExceptionMessage = "You can not create reservation before the current date !";
  }
}
