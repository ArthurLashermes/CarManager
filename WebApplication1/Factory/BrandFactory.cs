using Server.Controllers;
using Server.Domain;
using Shared.DeserializeModels;
using Shared.SerializeModels;
using WebApplication1;

namespace Server.Factory
{
    public class BrandFactory : IFactory
    {

        public IDeserializeModel DomainToDeserializeModel(IDomain domain)
        {
            var brand = (Brand)domain;
            var newBrand = new BrandModelDeserialize()
            {
                Id = brand.Id,
                Name = brand.Name,
            };
            return newBrand;
        }

        public IDomain SerializeModelToDomain(ISerializeModelSerialize serializeModel, IDomain domain)
        {
            var brand = (Brand)domain;
            var brandModelSerialize = (BrandModelSerialize)serializeModel;
            brand.Name = brandModelSerialize.Name;
            return brand;
        }
    }
}
