using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Domain;
using Server.Factory;
using Server.Services;
using Shared.DeserializeModels;
using Shared.SerializeModels;
using WebApplication1;

namespace Server.Controllers
{
	[Route("api/[controller]")]
	public class MaintenanceController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly ILogger<MaintenanceController> _logger;
        private readonly MaintenanceFactory _factory;
        private readonly MaintenanceService _maintenanceService;


        public MaintenanceController(ApplicationDbContext context, ILogger<MaintenanceController> logger, MaintenanceFactory maintenanceFactory, MaintenanceService maintenanceService)
        {
			_context = context;
			_logger = logger;
            _factory = maintenanceFactory;
            _maintenanceService = maintenanceService;
        }

		[HttpGet]
		public async Task<ActionResult<IEnumerable<MaintenanceModelDeserialize>>> GetMaintenances()
		{
			_logger.LogInformation("GetMaintenances Method");
            var maintenances = await _context.Maintenances
                .Select(x => _factory.DomainToDeserializeModel(x))
                .Cast<MaintenanceModelDeserialize>()
                .ToListAsync();
            return Ok(maintenances);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaintenanceModelDeserialize>> GetMaintenance(int id)
        {
            var maintenanceRepository = _context.Set<Maintenance>();

            var maintenance = maintenanceRepository
                .FirstOrDefault(x => x.Id == id);

            if (maintenance == null)
            {
                return NotFound();
            }

            return Ok(_factory.DomainToDeserializeModel(maintenance));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditMaintenance([FromBody] MaintenanceModelSerialize MaintenanceToEdit, int id)
        {
            var MaintenanceRepository = _context.Set<Maintenance>();

            var dbMaintenance = MaintenanceRepository
                .FirstOrDefault(x => x.Id == id);

            if (dbMaintenance == null)
            {
                _logger.LogWarning($"No Maintenance found with Id: {id}");
                return NotFound();
            }

            dbMaintenance = (Maintenance)_factory.SerializeModelToDomain(MaintenanceToEdit, dbMaintenance);

            MaintenanceRepository.Update(dbMaintenance);

            _context.SaveChanges();
            _logger.LogInformation($"The Maintenance with Id: {dbMaintenance.Id} has been edited");
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMaintenance([FromBody] MaintenanceModelSerialize MaintenanceToCreate)
        {
            var newMaintenance = (Maintenance)_factory.SerializeModelToDomain(MaintenanceToCreate, new Maintenance());

            _maintenanceService.ValidateMileageAtMaintenance(newMaintenance);

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
