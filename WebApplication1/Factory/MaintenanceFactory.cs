using Server.Domain;
using Shared.DeserializeModels;
using Shared.SerializeModels;

namespace Server.Factory
{
	public class MaintenanceFactory : IFactory
    {
        public IDeserializeModel DomainToDeserializeModel(IDomain domain)
        {
            var maintenance = (Maintenance)domain;
            var newMaintenance = new MaintenanceModelDeserialize()
            {
                Id = maintenance.Id,
                MileageAtMaintenance = maintenance.MileageAtMaintenance,
                WorkDone = maintenance.WorkDone,
            };
            return newMaintenance;
        }

        public IDomain SerializeModelToDomain(ISerializeModelSerialize serializeModel, IDomain domain)
        {
            var maintenance = (Maintenance)domain;
            var maintenanceModelSerialize = (MaintenanceModelSerialize)serializeModel;
            maintenance.MileageAtMaintenance = maintenanceModelSerialize.MileageAtMaintenance;
            maintenance.VehicleId = maintenanceModelSerialize.VehicleId;
            maintenance.WorkDone = maintenanceModelSerialize.WorkDone;
            return maintenance;
        }
    }
}
