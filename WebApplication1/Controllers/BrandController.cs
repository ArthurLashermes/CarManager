using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Domain;
using WebApplication1;
using Shared.ApiModels;


namespace Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BrandController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<BrandController> _logger;

		public BrandController(ApplicationDbContext context, ILogger<BrandController> logger)
		{
			_context = context;
			_logger = logger;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Brand>>> GetBrands()
		{
			_logger.LogInformation("GetBrand Method");
			return await _context.Brands.ToListAsync();
		}

        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            var brand = await _context.Brands.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] BrandModel BrandToCreate)
        {
            var newBrand = new Brand()
            {
                Name = BrandToCreate.Name,
            };

            var BrandRepository = _context.Set<Brand>();

            BrandRepository.Add(newBrand);

            _context.SaveChanges();
            return Ok();

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditBrand([FromBody] BrandModel BrandToEdit, int id)
        {
            var BrandRepository = _context.Set<Brand>();

            var dbBrand = BrandRepository
                .FirstOrDefault(x => x.Id == id);

            if (dbBrand == null)
            {
                _logger.LogWarning($"No Brand found with Id: {id}");
                return NotFound();
            }

            dbBrand.Name = BrandToEdit.Name;

            BrandRepository.Update(dbBrand);

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
