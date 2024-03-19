using Shared.Enum;
using System.ComponentModel.DataAnnotations;

namespace Shared.DeserializeModels
{
    public class VehicleModelDeserialize : IDeserializeModel
    {
		public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public int Year { get; set; }
        public int MaintenanceDelay { get; set; }
		public int Mileage { get; set; }
        public EnergyTypeEnum EnergyType { get; set; }
		public CarModelDeserialize Car { get; set; }
		public virtual ICollection<MaintenanceModelDeserialize> Maintenances { get; set; }
	}
}
