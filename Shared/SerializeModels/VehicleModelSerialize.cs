using Shared.Enum;

namespace Shared.SerializeModels;

public class VehicleModelSerialize : ISerializeModelSerialize
{
    public string RegistrationNumber { get; set; }
    public int Year { get; set; }
    public int Mileage { get; set; }
	public EnergyTypeEnum EnergyType { get; set; }
    public int CarId { get; set; }
}