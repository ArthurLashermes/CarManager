using Server.Domain;
using Assert = NUnit.Framework.Assert;

namespace TestProject;


[TestClass]
public class VehicleTest
{
    [TestMethod]
    public void ValidateVehicle_RegistrationNumberTooShort_Throw()
    {
        // Arrange
        var newVehicle = new Vehicle() { };

        //Assert
        Assert.Throws<ArgumentException>(() => newVehicle.RegistrationNumber = "788FR");
    }

    [TestMethod]
    public void ValidateVehicle_RegistrationNumberTooLong_Throw()
    {
        // Arrange
        var newVehicle = new Vehicle() { };

        //Assert
        Assert.Throws<ArgumentException>(() => newVehicle.RegistrationNumber = "788FR998FSFFF");
    }

    [TestMethod]
    public void ValidateVehicle_RegistrationNumberGoodLength_NotThrow()
    {
        // Arrange
        var newVehicle = new Vehicle() { };

        //Assert
        Assert.DoesNotThrow(() => newVehicle.RegistrationNumber = "78SFFFP7");
    }

    [TestMethod]
    public void ValidateVehicle_NegativeMileage_Throw()
    {
        // Arrange
        var newVehicle = new Vehicle() { };

        //Assert
        Assert.Throws<ArgumentException>(() => newVehicle.Mileage = -10);
    }

    [TestMethod]
    public void ValidateVehicle_PositiveMileage_NotThrow()
    {
        // Arrange
        var newVehicle = new Vehicle() { };

        //Assert
        Assert.DoesNotThrow(() => newVehicle.Mileage = 10);
    }

}