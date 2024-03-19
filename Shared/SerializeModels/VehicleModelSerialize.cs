using Shared.Enum;
using System.ComponentModel.DataAnnotations;

namespace Shared.SerializeModels;

public class VehicleModelSerialize : ISerializeModelSerialize
{
    public string RegistrationNumber { get; set; }
    public int Year { get; set; }
    [Required(ErrorMessage = "Le kilométrage du véhicule est requis.")]
    public int Mileage { get; set; }
    [Required(ErrorMessage = "Le type d'énergie qu'utilise le véhicule est requis.")]
	public EnergyTypeEnum EnergyType { get; set; }
    public int CarId { get; set; }
}