using Microsoft.EntityFrameworkCore;
using Server.Domain;
using Shared.DeserializeModels;
using Shared.SerializeModels;
using WebApplication1;

namespace Server.Factory
{
    public class VehicleFactory : IFactory
    {
        private readonly CarFactory _carFactory;
        private readonly MaintenanceFactory _maintenanceFactory;


        public VehicleFactory(CarFactory carFactory, MaintenanceFactory maintenanceFactory)
        {
            _carFactory = carFactory;
            _maintenanceFactory = maintenanceFactory;
        }
        public IDeserializeModel DomainToDeserializeModel(IDomain domain)
        {
            var vehicle = (Vehicle)domain;
            var newVehicle = new VehicleModelDeserialize()
            {
                Id = vehicle.Id,
                RegistrationNumber = vehicle.RegistrationNumber,
                Year = vehicle.Year,
                Mileage = vehicle.Mileage,
                EnergyType = vehicle.EnergyType,
                Car = (CarModelDeserialize)_carFactory.DomainToDeserializeModel(vehicle.Car),
                Maintenances = vehicle.Maintenances
                    .Select(x => _maintenanceFactory.DomainToDeserializeModel(x))
                    .Cast<MaintenanceModelDeserialize>()
                    .ToList(),
            };
            return newVehicle;
        }

        public IDomain SerializeModelToDomain(ISerializeModelSerialize serializeModel, IDomain domain)
        {
            var vehicle = (Vehicle)domain;
            var vehicleModelSerialize = (VehicleModelSerialize)serializeModel;
            vehicle.CarId = vehicleModelSerialize.CarId;
            vehicle.EnergyType = vehicleModelSerialize.EnergyType;
            vehicle.Mileage = vehicleModelSerialize.Mileage;
            vehicle.RegistrationNumber = vehicleModelSerialize.RegistrationNumber;
            vehicle.Year = vehicleModelSerialize.Year;
            return vehicle;
        }
    }
}
