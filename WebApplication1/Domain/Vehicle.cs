namespace Server.Domain
{
	public class Vehicle
	{
		public int Id { get; set; }

		private string _registrationNumber;
		public string RegistrationNumber
		{
			get => _registrationNumber;
			set
			{
				if (value.Length < 7 || value.Length > 9)
					throw new ArgumentException("Le format de l’immatriculation doit être entre 7 et 9 caractères.");
				_registrationNumber = value;
			}
		}
		public int Year { get; set; }

		private int _mileage;
		public int Mileage
		{
			get => _mileage;
			set
			{
				if (value < 0)
					throw new ArgumentException("Le nombre de KM doit être positif.");
				_mileage = value;
			}
		}
		public string EnergyType { get; set; }
		public int CarModelId { get; set; }
		public CarModel CarModel { get; set; }
		public virtual ICollection<Maintenance> Maintenances { get; set; }
	}
}
