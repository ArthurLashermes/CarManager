using Server.Domain;
using Assert = NUnit.Framework.Assert;

namespace TestProject;

[TestClass]
public class CarTest
{
    [TestMethod]
    public void ValidateCar_EmptyName_ReturnError_0()
    {
        // Arrange
        var newCar = new Car() { };

        //Assert
        Assert.Throws<ArgumentException>(() => newCar.Name = "");
    }

    [TestMethod]
    public void ValidateCar_EmptyName_Throws_1()
    {
        // Arrange
        var newCar = new Car() { };

        //Assert
        Assert.Throws<ArgumentException>(() => newCar.Name = " ");
    }

    [TestMethod]
    public void ValidateCar_EmptyName_Throws_2()
    {
        // Arrange
        var newCar = new Car() { };

        //Assert
        Assert.Throws<ArgumentException>(() => newCar.Name = null);
    }

    [TestMethod]
    public void ValidateCar_NotEmptyName_NotThrow()
    {
        // Arrange
        var newCar = new Car() { };
        
        //Assert
        Assert.DoesNotThrow(() => newCar.Name = "test");
    }

}