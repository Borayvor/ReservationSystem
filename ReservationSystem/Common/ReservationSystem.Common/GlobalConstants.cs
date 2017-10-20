namespace ReservationSystem.Common
{
  public class GlobalConstants
  {
    public const string AdministratorRoleName = "Administrator";

    public const string CalendarTitle = "Select Date";
    public const string CalendarSelectedDateMessage = "You have selected:";

    public const string DateFormat = "{0:dd-MM-yyyy}";

    //// Exception Messages
    public const string AutoMapper_Required_ExceptionMessage = "An instance of AutoMapper is required !";
    public const string EfDbRepositoryOfReservation_Required_ExceptionMessage = "An instance of EfDbRepository of Reservation is required !";
    public const string GetById_GuidEmpty_ExceptionMessage = "Id must not be empty Guid !";
    public const string Reservation_Required_ExceptionMessage = "An instance of Reservation is required !";
    public const string UnitOfWork_Required_ExceptionMessage = "An instance of UnitOfWork is required !";

    //// Exception Messages - ReservationService
    public const string ReservationService_Required_ExceptionMessage = "An instance of ReservationService is required !";
    public const string ReservationService_GetActiveByDate_ActiveDate_ExceptionMessage = "You can not get reservation before the current date !";
    public const string ReservationService_Create__ActiveDate_ExceptionMessage = "You can not create reservation before the current date !";
  }
}
