using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Domain;
using WebApplication1;

namespace Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CarModelController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger <CarModelController> _logger;

        public CarModelController(ApplicationDbContext context, ILogger<CarModelController> logger)
        {
            _context = context;
			_logger = logger;
        }

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CarModel>>> GetCarModels() 
		{
			_logger.LogInformation("GetCarModels Method");
			return await _context.CarModels.ToListAsync();
		}
    }
}
