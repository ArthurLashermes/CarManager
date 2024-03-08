using Microsoft.EntityFrameworkCore;
using Server.Domain;
using Shared.DeserializeModels;
using Shared.SerializeModels;
using WebApplication1;

namespace Server.Factory
{
    public class CarFactory : IFactory
    {
        private readonly BrandFactory _brandFactory;

        public CarFactory(BrandFactory brandFactory)
        {
            _brandFactory = brandFactory;
        }
        public IDeserializeModel DomainToDeserializeModel(IDomain domain)
        {
            var car = (Car)domain;
            var newCar = new CarModelDeserialize()
            {
                Id = car.Id,
                Name = car.Name,
                Brand = (BrandModelDeserialize) _brandFactory.DomainToDeserializeModel(car.Brand),
                MaintenanceFrequency = car.MaintenanceFrequency,
            };
            return newCar;
        }

        public IDomain SerializeModelToDomain(ISerializeModelSerialize serializeModel, IDomain domain)
        {
            var brand = (Car)domain;
            var brandModelSerialize = (CarModelSerialize)serializeModel;
            brand.Name = brandModelSerialize.Name;
            brand.BrandId = brandModelSerialize.BrandId;
            brand.MaintenanceFrequency = brandModelSerialize.MaintenanceFrequency;
            return brand;
        }
    }
}
