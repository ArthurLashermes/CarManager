using Server.Domain;
using Server.Services;

namespace TestProject;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Server.Controllers;
using Server.Factory;
using TestProject.Mock;
using WebApplication1;
using Assert = NUnit.Framework.Assert;


[TestClass]
public class MaintenanceTest
{
    [TestMethod]
    public void ValidateMaintenance_EmptyName_Throw_0()
    {
        // Arrange
        var newMaintenance = new Maintenance() { };

        //Assert
        Assert.Throws<ArgumentException>(() => newMaintenance.WorkDone = "");
    }

    [TestMethod]
    public void ValidateMaintenance_EmptyName_Throw_1()
    {
        // Arrange
        var newMaintenance = new Maintenance() { };

        //Assert
        Assert.Throws<ArgumentException>(() => newMaintenance.WorkDone = null);
    }


    [TestMethod]
    public void ValidateMaintenance_NotEmptyName_NotThrow()
    {
        // Arrange
        var newMaintenance = new Maintenance() { };

        //Assert
        Assert.DoesNotThrow(() => newMaintenance.WorkDone = "test");
    }

    [TestMethod]
    public void ValidateMaintenance_CurrentKmIsNotSameThanMaintenance_Throw()
    {
        // Arrange
        var newMaintenance = new Maintenance() { };
        var newVehicle = new Vehicle() { };
        newVehicle.Mileage = 100000;
        newMaintenance.MileageAtMaintenance = 120000;
        var mockSet = new Mock<DbSet<Vehicle>>();
        var mockDbContext = new Mock<ApplicationDbContextMocked>();
        mockDbContext.Setup(m => m.Vehicles).Returns(mockSet.Object);
        var _maintenanceService = new MaintenanceServiceMocked(mockDbContext.Object);
        newMaintenance.Vehicle = newVehicle;
        //Assert
        Assert.Throws<ArgumentException>(() => _maintenanceService.ValidateMileageAtMaintenance(newMaintenance));
    }

    [TestMethod]
    public void ValidateMaintenance_CurrentKmIsSameThanMaintenance_NotThrow()
    {
        // Arrange
        var newMaintenance = new Maintenance() { };
        var newVehicle = new Vehicle() { };
        newVehicle.Mileage = 100000;
        newMaintenance.MileageAtMaintenance = 100000;
        var mockSet = new Mock<DbSet<Vehicle>>();
        var mockDbContext = new Mock<ApplicationDbContextMocked>();
        mockDbContext.Setup(m => m.Vehicles).Returns(mockSet.Object);
        var _maintenanceService = new MaintenanceServiceMocked(mockDbContext.Object);
        newMaintenance.Vehicle = newVehicle;

        //Assert
        Assert.DoesNotThrow(() => _maintenanceService.ValidateMileageAtMaintenance(newMaintenance));
    }

    [TestMethod]
    public void ValidateMaintenance_VehicleIsNull_Throw()
    {
        // Arrange
        var newMaintenance = new Maintenance() { };

        var mockSet = new Mock<DbSet<Vehicle>>();
        var mockDbContext = new Mock<ApplicationDbContextMocked>();
        mockDbContext.Setup(m => m.Vehicles).Returns(mockSet.Object);
        var _maintenanceService = new MaintenanceServiceMocked(mockDbContext.Object);

        //Assert
        Assert.Throws<InvalidOperationException>(() => _maintenanceService.CalculateMaintenanceDelay(newMaintenance));
    }

    [TestMethod]
    public void ValidateMaintenance_CarIsNull_Throw()
    {
        // Arrange
        var newMaintenance = new Maintenance() { };
        var newVehicle = new Vehicle() { };

        var mockSet = new Mock<DbSet<Vehicle>>();
        var mockDbContext = new Mock<ApplicationDbContextMocked>();
        mockDbContext.Setup(m => m.Vehicles).Returns(mockSet.Object);
        var _maintenanceService = new MaintenanceServiceMocked(mockDbContext.Object);
        newMaintenance.Vehicle = newVehicle;
        //Assert
        Assert.Throws<InvalidOperationException>(() => _maintenanceService.CalculateMaintenanceDelay(newMaintenance));
    }

    [TestMethod]
    public void ValidateMaintenance_ReturnGoodResult_NotThrow()
    {
        // Arrange
        var newMaintenance = new Maintenance() { };
        var newVehicle = new Vehicle() { };
        var newCar = new Car() { };

        var mockSet = new Mock<DbSet<Vehicle>>();
        var mockDbContext = new Mock<ApplicationDbContextMocked>();
        mockDbContext.Setup(m => m.Vehicles).Returns(mockSet.Object);
        var _maintenanceService = new MaintenanceServiceMocked(mockDbContext.Object);

        newMaintenance.MileageAtMaintenance = 60000;
        newCar.MaintenanceFrequency = 30000;
        newVehicle.Mileage = 60000;
        newVehicle.Car = newCar;
        newMaintenance.Vehicle = newVehicle;
        //Assert
        Assert.DoesNotThrow(() => _maintenanceService.CalculateMaintenanceDelay(newMaintenance));
    }
}