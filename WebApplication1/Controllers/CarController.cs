using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Server.Domain;
using System.Reflection.Metadata;
using Shared.SerializeModels;
using WebApplication1;

namespace Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CarController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger <CarController> _logger;

        public CarController(ApplicationDbContext context, ILogger<CarController> logger)
        {
            _context = context;
			_logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var carRepository = _context.Set<Car>();

            var car = carRepository
                    .Include(car => car.Brand)
                    .Include(car => car.Vehicles)
                    .FirstOrDefault(x => x.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpGet]
		public async Task<ActionResult<IEnumerable<Car>>> GetCars() 
		{
			_logger.LogInformation("GetCar Method");
            return await _context.Cars.Include(car => car.Brand).Include(car => car.Vehicles).ToListAsync();
		}

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCar([FromBody] Shared.SerializeModels.CarModelSerialize CarToEdit, int id)
        {
            var CarRepository = _context.Set<Car>();

            var dbCar = CarRepository
                .FirstOrDefault(x => x.Id == id);

            if (dbCar == null)
            {
                _logger.LogWarning($"No Car found with Id: {id}");
                return NotFound();
            }

            dbCar.Name = CarToEdit.Name;
            dbCar.BrandId = CarToEdit.BrandId;
            dbCar.MaintenanceFrequency = CarToEdit.MaintenanceFrequency;

            CarRepository.Update(dbCar);

            _context.SaveChanges();
            _logger.LogInformation($"The Car with Id: {dbCar.Id} and name: {dbCar.Name} has been edited");
            return Ok();

        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] Shared.SerializeModels.CarModelSerialize CarToCreate)
        {
            var newCar = new Car()
            {
                Name = CarToCreate.Name,
                BrandId = CarToCreate.BrandId,
                MaintenanceFrequency = CarToCreate.MaintenanceFrequency,
            };

            var carRepository = _context.Set<Car>();

            carRepository.Add(newCar);

            _context.SaveChanges();
            return Ok();

        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
