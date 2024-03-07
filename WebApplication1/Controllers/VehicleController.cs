using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Domain;
using Server.Factory;
using Shared.DeserializeModels;
using Shared.SerializeModels;
using WebApplication1;

namespace Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VehicleController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<VehicleController> _logger;
        private readonly VehicleFactory _factory;

        public VehicleController(ApplicationDbContext context, ILogger<VehicleController> logger,VehicleFactory vehicleFactory)
		{
			_context = context;
			_logger = logger;
            _factory = vehicleFactory;

        }


		[HttpGet]
		public async Task<ActionResult<IEnumerable<VehicleModelDeserialize>>> GetVehicles()
		{
			_logger.LogInformation("GetVehicles Method");

            var vehicles = await _context.Vehicles
                .Include(b => b.Car)
                .Include(b => b.Maintenances)
                .Select(x => _factory.DomainToDeserializeModel(x))
                .Cast<VehicleModelDeserialize>()
                .ToListAsync();

            return Ok(vehicles);
		}



        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var vehicleRepository = _context.Set<Vehicle>();

            var vehicle = vehicleRepository
                .FirstOrDefault(x => x.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditVehicle([FromBody] Shared.SerializeModels.VehicleModelSerialize vehicleToEdit, int id)
        {
            var vehicleRepository = _context.Set<Vehicle>();

            var dbVehicle = vehicleRepository
                .FirstOrDefault(x => x.Id == id);

            if (dbVehicle == null)
            {
                _logger.LogWarning($"No Vehicle found with Id: {id}");
                return NotFound();
            }

            dbVehicle.CarId = vehicleToEdit.CarId;
            dbVehicle.EnergyType = vehicleToEdit.EnergyType;
            dbVehicle.Mileage = vehicleToEdit.Mileage;
            dbVehicle.RegistrationNumber = vehicleToEdit.RegistrationNumber;
            dbVehicle.Year = vehicleToEdit.Year;


            vehicleRepository.Update(dbVehicle);

            _context.SaveChanges();
            _logger.LogInformation($"The Vehicle with Id: {dbVehicle.Id} has been edited");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] Shared.SerializeModels.VehicleModelSerialize vehicleToCreate)
        {
            var newVehicle = new Vehicle()
            {
                CarId = vehicleToCreate.CarId,
                EnergyType = vehicleToCreate.EnergyType,
                Mileage = vehicleToCreate.Mileage,
                RegistrationNumber = vehicleToCreate.RegistrationNumber,
                Year = vehicleToCreate.Year,
            };

            var vehicleRepository = _context.Set<Vehicle>();

            vehicleRepository.Add(newVehicle);

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteCar(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
