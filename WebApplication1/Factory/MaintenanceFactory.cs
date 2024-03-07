using Server.Domain;
using Shared.DeserializeModels;
using Shared.SerializeModels;
using System.Drawing;

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

        public IDomain SerializeModelToDomain(ISerializeModelSerialize domain)
        {
            throw new NotImplementedException();
        }
    }
}
