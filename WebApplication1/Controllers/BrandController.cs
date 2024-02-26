using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Domain;
using WebApplication1;

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
	}
}
