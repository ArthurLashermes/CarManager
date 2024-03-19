using Shared.DeserializeModels;

namespace Shared.DeserializeModels
{
    public class CarModelDeserialize : IDeserializeModel
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public BrandModelDeserialize Brand { get; set; }
		public int MaintenanceFrequency { get; set; }
	}
}
