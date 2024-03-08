namespace Server.Domain
{
	public class Maintenance : IDomain
    {
		public int Id { get; set; }
		public int VehicleId { get; set; }
		public Vehicle Vehicle { get; set; }

		public int MileageAtMaintenance { get; set; }

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
	}
}
