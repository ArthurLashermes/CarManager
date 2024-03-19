using Shared.Enum;
using System.ComponentModel.DataAnnotations;

namespace Server.Domain
{
	public class Vehicle : IDomain
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
		public EnergyTypeEnum EnergyType { get; set; }
		public int CarId { get; set; }
		public Car Car { get; set; }
		public virtual ICollection<Maintenance> Maintenances { get; set; }
	}
}
