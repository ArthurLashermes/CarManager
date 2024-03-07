using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Domain;
using Shared.SerializeModels;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Maintenance>> GetMaintenance(int id)
        {
            var maintenanceRepository = _context.Set<Maintenance>();

            var maintenance = maintenanceRepository
                .FirstOrDefault(x => x.Id == id);

            if (maintenance == null)
            {
                return NotFound();
            }

            return maintenance;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMaintenance([FromBody] Shared.SerializeModels.MaintenanceModelSerialize MaintenanceToEdit, int id)
        {
            var MaintenanceRepository = _context.Set<Maintenance>();

            var dbMaintenance = MaintenanceRepository
                .FirstOrDefault(x => x.Id == id);

            if (dbMaintenance == null)
            {
                _logger.LogWarning($"No Maintenance found with Id: {id}");
                return NotFound();
            }

            dbMaintenance. MileageAtMaintenance= MaintenanceToEdit.MileageAtMaintenance;
            dbMaintenance.VehicleId = MaintenanceToEdit.VehicleId;
            dbMaintenance.WorkDone = MaintenanceToEdit.WorkDone;

            MaintenanceRepository.Update(dbMaintenance);

            _context.SaveChanges();
            _logger.LogInformation($"The Maintenance with Id: {dbMaintenance.Id} has been edited");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMaintenance([FromBody] Shared.SerializeModels.MaintenanceModelSerialize MaintenanceToCreate)
        {
            var newMaintenance = new Maintenance()
            {
                MileageAtMaintenance = MaintenanceToCreate.MileageAtMaintenance,
                VehicleId = MaintenanceToCreate.VehicleId,
                WorkDone = MaintenanceToCreate.WorkDone,
            };

            var maintenanceRepository = _context.Set<Maintenance>();

            maintenanceRepository.Add(newMaintenance);

            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteCar(int id)
        {
            var maintenance = await _context.Maintenances.FindAsync(id);
            if (maintenance == null)
            {
                return NotFound();
            }

            _context.Maintenances.Remove(maintenance);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
