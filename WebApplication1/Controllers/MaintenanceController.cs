using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Domain;
using WebApplication1;

namespace Server.Controllers
{
	[Route("api/[controller]")]
	public class MaintenanceController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<MaintenanceController> _logger;

        public MaintenanceController(ApplicationDbContext context, ILogger<MaintenanceController> logger)
        {
			_context = context;
			_logger = logger;
        }

		[HttpGet]

		public async Task<ActionResult<IEnumerable<Maintenance>>> GetMaintenance()
		{
			_logger.LogInformation("GetMaintenance Method");
			return await _context.Maintenances.ToListAsync();
		}
    }
}
