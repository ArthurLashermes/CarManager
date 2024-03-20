namespace Server.Domain
{
	public class Car : IDomain
    {
		public int Id { get; set; }

		private string _name;
		public string Name
		{
			get => _name;
			set
			{
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentException("Le nom du modèle doit avoir au moins 1 charactère.");
				_name = value;
			}
		}
		public int BrandId { get; set; }
		public Brand Brand { get; set; }

		private int _maintenanceFrequency;
		public int MaintenanceFrequency
		{
			get => _maintenanceFrequency;
			set
			{
				if (value < 0)
					throw new ArgumentException("La fréquence d'entretien ne peut pas être négative");
				_maintenanceFrequency = value;
			}
		}

		public virtual ICollection<Vehicle> Vehicles { get; set; }
	}
}
