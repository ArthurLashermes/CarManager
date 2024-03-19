using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Server.Domain;
using System.Reflection.Metadata;
using Server.Factory;
using Shared.DeserializeModels;
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
        private readonly CarFactory _factory;

        public CarController(ApplicationDbContext context, ILogger<CarController> logger, CarFactory factory)
        {
            _context = context;
			_logger = logger;
            _factory = factory;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarModelDeserialize>> GetCar(int id)
        {
            var carRepository = _context.Set<Car>();

            var car = carRepository
                    .Include(car => car.Brand)
                    .FirstOrDefault(x => x.Id == id);

            if (car == null)
            {
                return NotFound();
            }
            

            return (CarModelDeserialize)_factory.DomainToDeserializeModel(car);
        }

        [HttpGet]
		public async Task<ActionResult<IEnumerable<CarModelDeserialize>>> GetCars() 
		{
			_logger.LogInformation("GetCar Method");
            var cars = await _context.Cars
                .Include(b => b.Brand)
                .ToListAsync();

            var deserializeCars = cars
                .Select(x => _factory.DomainToDeserializeModel(x))
                .Cast<CarModelDeserialize>()
                .ToList();

            return Ok(deserializeCars);
		}

        [HttpPut("{id}")]
        public async Task<IActionResult> EditCar([FromBody] CarModelSerialize CarToEdit, int id)
        {
            var CarRepository = _context.Set<Car>();

            var dbCar = CarRepository
                .FirstOrDefault(x => x.Id == id);

            if (dbCar == null)
            {
                _logger.LogWarning($"No Car found with Id: {id}");
                return NotFound();
            }
            
            CarRepository.Update((Car)_factory.SerializeModelToDomain(CarToEdit,dbCar));

            _context.SaveChanges();
            _logger.LogInformation($"The Car with Id: {dbCar.Id} and name: {dbCar.Name} has been edited");
            return Ok();

        }

        [HttpPost]
        public async Task<IActionResult> CreateCar([FromBody] CarModelSerialize CarToCreate)
        {
            var carRepository = _context.Set<Car>();

            carRepository.Add((Car)_factory.SerializeModelToDomain(CarToCreate, new Car()));

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

        /// <summary>
        /// Retourne les Cars selon un BrandId
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        [HttpGet("bybrand/{brandId}")]
        public async Task<ActionResult<IEnumerable<CarModelDeserialize>>> GetCarsByBrand(int brandId)
        {
            _logger.LogInformation($"Recherche des voitures de la marque Id: {brandId}");
            var carsByBrand = await _context.Cars
                .Where(c => c.BrandId == brandId)
                .Include(c => c.Brand)
                .ToListAsync();

            if(carsByBrand == null || !carsByBrand.Any())
            {
                return NotFound($"Aucune voiture trouvée pour la marque Id: {brandId}");
            }

            var deserializeCars = carsByBrand
                .Select(car => _factory.DomainToDeserializeModel(car))
                .Cast<CarModelDeserialize>()
                .ToList();

            return Ok(deserializeCars);

        }

        [HttpGet("exists/{name}")]
        public async Task<ActionResult<bool>> CarNameExists(string name, int? excludeCarId = null)
        {
            var carExists = await _context.Cars
                .Where(c => c.Name == name)
                .Where(c => !excludeCarId.HasValue || c.Id != excludeCarId.Value)
                .AnyAsync();

            return carExists;
        }

	}
}
