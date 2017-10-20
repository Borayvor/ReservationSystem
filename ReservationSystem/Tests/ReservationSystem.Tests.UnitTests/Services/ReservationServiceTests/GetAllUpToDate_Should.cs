using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ReservationSystem.Data.Common.Repositories;
using ReservationSystem.Data.Models;
using ReservationSystem.Services.Data;
using Xunit;

namespace ReservationSystem.Tests.UnitTests.Services.ReservationServiceTests
{
  public class GetAllUpToDate_Should
  {
    [Fact]
    public void CallEfDbRepository_GetAll_Method_Once()
    {
      // Arange       
      var mockedIEfDbRepository = new Mock<IEfDbRepository<Reservation>>();

      var service = new ReservationService(mockedIEfDbRepository.Object);

      // Act
      service.GetAllUpToDate();

      // Assert
      mockedIEfDbRepository.Verify(x => x.GetAll(), Times.Once);
    }

    [Fact]
    public void ReturnProperlyResult_When_ReservationList_IsNotEmpty()
    {
      // Arange         
      var mockedEntityFirst = new Reservation
      {
        DateOfReservation = DateTime.UtcNow.Date.AddDays(1),
        Id = Guid.Empty,
        Owner = new Mock<ApplicationUser>().Object,
        OwnerId = string.Empty,
        Price = 0
      };

      var mockedEntitySecond = new Reservation
      {
        DateOfReservation = DateTime.UtcNow.Date.AddDays(-1),
        Id = Guid.Empty,
        Owner = new Mock<ApplicationUser>().Object,
        OwnerId = string.Empty,
        Price = 0
      };

      var entityList = new List<Reservation>
      {
        mockedEntityFirst,
        mockedEntitySecond
      };

      var mockedIEfDbRepository = new Mock<IEfDbRepository<Reservation>>();
      mockedIEfDbRepository.Setup(x => x.GetAll()).Returns(entityList.AsQueryable());

      var service = new ReservationService(mockedIEfDbRepository.Object);

      // Act
      var actual = service.GetAllUpToDate().ToList();

      // Assert
      Assert.Contains(mockedEntityFirst, actual);
      Assert.DoesNotContain(mockedEntitySecond, actual);
    }
  }
}
