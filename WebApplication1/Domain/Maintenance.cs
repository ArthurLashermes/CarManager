namespace Server.Domain
{
	public class Maintenance
	{
		public int Id { get; set; }
		public int VehicleId { get; set; }
		public Vehicle Vehicle { get; set; }

		private int _mileageAtMaintenance;
		public int MileageAtMaintenance
		{
			get => _mileageAtMaintenance;
			set
			{
				if (Vehicle != null && value != Vehicle?.Mileage)
					throw new ArgumentException("Lors de l’ajout de travaux, le kilométrage doit être le kilométrage courant du véhicule.");
				_mileageAtMaintenance = value;
			}
		}

		private string _workDone;
		public string WorkDone
		{
			get => _workDone;
			set
			{
				if(string.IsNullOrEmpty(value))
					throw new ArgumentException("La description des travaux effectués doit avoir au moins 1 caractère.");
				_workDone = value;
			}
		}

		/// <summary>
		/// Méthode pour calculer le retard d'entretien
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public int CalculateMaintenanceDelay()
		{
			if (Vehicle == null || Vehicle.Car == null)
				throw new InvalidOperationException("Les informations du véhicule ou du modèle de voiture sont manquantes.");

			return MileageAtMaintenance + Vehicle.Car.MaintenanceFrequency - Vehicle.Mileage;
		}
	}
}
