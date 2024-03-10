using Server.Domain;
using Shared.DeserializeModels;
using Shared.SerializeModels;

namespace Server.Factory
{
    public interface IFactory
    {
        public IDeserializeModel DomainToDeserializeModel(IDomain domain);

        public IDomain SerializeModelToDomain(ISerializeModelSerialize serializeModel, IDomain domain);
    }
}
