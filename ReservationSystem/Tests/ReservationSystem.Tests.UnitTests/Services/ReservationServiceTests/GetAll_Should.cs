﻿using System.Collections.Generic;
using System.Linq;
using Moq;
using ReservationSystem.Data.Common.Repositories;
using ReservationSystem.Data.Models;
using ReservationSystem.Services.Data;
using Xunit;

namespace ReservationSystem.Tests.UnitTests.Services.ReservationServiceTests
{
  public class GetAll_Should
  {
    [Fact]
    public void CallEfDbRepository_GetAll_Method_Once()
    {
      // Arange       
      var mockedIEfDbRepository = new Mock<IEfDbRepository<Reservation>>();

      var service = new ReservationService(mockedIEfDbRepository.Object);

      // Act
      service.GetAll();

      // Assert
      mockedIEfDbRepository.Verify(x => x.GetAll(), Times.Once);
    }

    [Fact]
    public void ReturnProperlyResult_When_ReservationList_IsNotEmpty()
    {
      // Arange         
      var mockedEntityFirst = new Mock<Reservation>();
      var mockedEntitySecond = new Mock<Reservation>();

      var entityList = new List<Reservation>
      {
        mockedEntityFirst.Object,
        mockedEntitySecond.Object
      };

      var mockedIEfDbRepository = new Mock<IEfDbRepository<Reservation>>();
      mockedIEfDbRepository.Setup(x => x.GetAll()).Returns(entityList.AsQueryable());

      var service = new ReservationService(mockedIEfDbRepository.Object);

      // Act
      var actual = service.GetAll().ToList();

      // Assert
      Assert.Contains(mockedEntityFirst.Object, actual);
      Assert.Contains(mockedEntitySecond.Object, actual);
    }
  }
}
