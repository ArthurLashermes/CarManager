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
                .Include(b => b.Car.Brand)
                .ToListAsync();

            var deserializeVehicles = vehicles
                .Select(x => _factory.DomainToDeserializeModel(x))
                .Cast<VehicleModelDeserialize>()
                .ToList();

            return Ok(deserializeVehicles);
		}



        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleModelDeserialize>> GetVehicle(int id)
        {
            var vehicleRepository = _context.Set<Vehicle>();

            var vehicle = vehicleRepository
                .Include(b => b.Car)
                .Include(b => b.Maintenances)
                .Include(b => b.Car.Brand)
                .FirstOrDefault(x => x.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }

            return (VehicleModelDeserialize)_factory.DomainToDeserializeModel(vehicle);
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

            dbVehicle = (Vehicle)_factory.SerializeModelToDomain(vehicleToEdit, dbVehicle);

            vehicleRepository.Update(dbVehicle);

            _context.SaveChanges();
            _logger.LogInformation($"The Vehicle with Id: {dbVehicle.Id} has been edited");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleModelSerialize vehicleToCreate)
        {
            var newVehicle = (Vehicle)_factory.SerializeModelToDomain(vehicleToCreate, new Vehicle());

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
