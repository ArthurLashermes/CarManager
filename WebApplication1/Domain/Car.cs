namespace Server.Domain
{
	public class Car
	{
		public int Id { get; set; }

		private string _name;
		public string Name
		{
			get => _name;
			set
			{
				if (string.IsNullOrWhiteSpace(value))
					throw new ArgumentException("Le nom du modèle doit avoir au moins 1 caractère.");
				_name = value;
			}
		}
		public int BrandId { get; set; }
		public Brand Brand { get; set; }
		public int MaintenanceFrequency { get; set; }
		public virtual ICollection<Vehicle> Vehicles { get; set; }
	}
}
