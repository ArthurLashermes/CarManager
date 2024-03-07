using Shared.DeserializeModels;

namespace Shared.DeserializeModels
{
    public class MaintenanceModelDeserialize : IDeserializeModel
    {
		public int Id { get; set; }

		private int _mileageAtMaintenance;
		public int MileageAtMaintenance { get; set; }

        private string _workDone;
		public string WorkDone { get; set; }
	}
}
