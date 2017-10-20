using System;
using Moq;
using ReservationSystem.Common;
using ReservationSystem.Data.Common.Repositories;
using ReservationSystem.Data.Models;
using ReservationSystem.Services.Data;
using Xunit;

namespace ReservationSystem.Tests.UnitTests.Services.ReservationServiceTests
{
  public class Constructor_Should
  {
    [Fact]
    public void Throw_ArgumentNullException_WithProperMessage_When_ReservationSystemEfRepositoryOfReservation_IsNull()
    {
      // Arange, Act & Assert
      Assert.Throws<ArgumentNullException>(
        GlobalConstants.EfDbRepositoryOfReservationRequiredExceptionMessage,
        () => new ReservationService(null));
    }

    [Fact]
    public void NotThrow_WhenArguments_AreValid()
    {
      // Arange      
      var mockedIEfDbRepository = new Mock<IEfDbRepository<Reservation>>();

      // Act & Assert
      new ReservationService(mockedIEfDbRepository.Object);
    }
  }
}
