using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Domain;
using Shared.ApiModels;
using WebApplication1;

namespace Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VehicleController : ControllerBase
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<VehicleController> _logger;

		public VehicleController(ApplicationDbContext context, ILogger<VehicleController> logger)
		{
			_context = context;
			_logger = logger;
		}


		[HttpGet]
		public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
		{
			_logger.LogInformation("GetVehicles Method");

			return await _context.Vehicles.ToListAsync();
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
        public async Task<IActionResult> EditVehicle([FromBody] VehicleModel vehicleToEdit, int id)
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
        public async Task<IActionResult> CreateVehicle([FromBody] VehicleModel vehicleToCreate)
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
