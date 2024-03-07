
using Shared.DeserializeModels;

namespace Shared.DeserializeModels
{
    public class VehicleModelDeserialize : IDeserializeModel
    {
		public int Id { get; set; }

		private string _registrationNumber;
		public string RegistrationNumber { get; set; }
        public int Year { get; set; }

		public int Mileage { get; set; }
        public string EnergyType { get; set; }
		public int CarId { get; set; }
		public CarModelDeserialize Car { get; set; }
		public virtual ICollection<MaintenanceModelDeserialize> Maintenances { get; set; }
	}
}
