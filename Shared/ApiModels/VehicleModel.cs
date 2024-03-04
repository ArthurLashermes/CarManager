namespace Shared.ApiModels;

public class VehicleModel
{
    public string RegistrationNumber { get; set; }
    public int Year { get; set; }

    public int Mileage { get; set; }
    public string EnergyType { get; set; }
    public int CarId { get; set; }
}