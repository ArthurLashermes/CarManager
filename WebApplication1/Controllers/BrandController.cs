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
	public class BrandController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<BrandController> _logger;
        private readonly BrandFactory _factory;


        public BrandController(ApplicationDbContext context, ILogger<BrandController> logger, BrandFactory factory)
		{
			_context = context;
			_logger = logger;
            _factory = factory;

        }

		[HttpGet]
		public async Task<ActionResult<IEnumerable<BrandModelDeserialize>>> GetBrands()
		{
			_logger.LogInformation("GetBrand Method");

            var brands = await _context.Brands
                .Include(b => b.Car)
                .Select(x => _factory.DomainToDeserializeModel(x))
                .Cast<BrandModelDeserialize>()
                .ToListAsync();

            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Shared.DeserializeModels.BrandModelDeserialize>> GetBrand(int id)
        {
            var brand = _context.Brands
                .Include(b => b.Car)
                .FirstOrDefault(x => x.Id == id);

            if (brand == null)
            {
                return NotFound();
            }

            return (Shared.DeserializeModels.BrandModelDeserialize) _factory.DomainToDeserializeModel(brand);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] Shared.SerializeModels.BrandModelSerialize BrandToCreate)
        {
            var newBrand = (Brand)_factory.SerializeModelToDomain(BrandToCreate, new Brand());

            var BrandRepository = _context.Set<Brand>();

            BrandRepository.Add(newBrand);

            _context.SaveChanges();
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditBrand([FromBody] Shared.SerializeModels.BrandModelSerialize BrandToEdit, int id)
        {
            var BrandRepository = _context.Set<Brand>();

            var dbBrand = BrandRepository
                .FirstOrDefault(x => x.Id == id);

            if (dbBrand == null)
            {
                _logger.LogWarning($"No Brand found with Id: {id}");
                return NotFound();
            }


            BrandRepository.Update((Brand)_factory.SerializeModelToDomain(BrandToEdit,dbBrand));

            _context.SaveChanges();
            _logger.LogInformation($"The Brand with Id: {dbBrand.Id} and name: {dbBrand.Name} has been edited");
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
