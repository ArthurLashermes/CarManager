using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Domain;
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


		// GET: api/Vehicles
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Vehicle>>> GetVehicles()
		{
			_logger.LogInformation("GetVehicles Method");

			return await _context.Vehicles.ToListAsync();
		}
	}
}
