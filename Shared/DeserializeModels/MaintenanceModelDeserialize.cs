namespace Shared.DeserializeModels
{
	public class MaintenanceModelDeserialize : IDeserializeModel
    {
		public int Id { get; set; }
		public int MileageAtMaintenance { get; set; }
		public string WorkDone { get; set; }
	}
}
