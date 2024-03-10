namespace Shared.SerializeModels;

public class CarModelSerialize : ISerializeModelSerialize
{
        public string Name { get; set; }
        public int BrandId { get; set; }
        public int MaintenanceFrequency { get; set; }
}