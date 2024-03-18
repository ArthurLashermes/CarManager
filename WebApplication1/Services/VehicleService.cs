using Microsoft.EntityFrameworkCore;
using Shared.DeserializeModels;
using WebApplication1;

namespace Server.Services
{
    public class VehicleService
    {
        private readonly ApplicationDbContext _context;

        public VehicleService(ApplicationDbContext context)
        {
            _context = context; 
        }

        
    }
}
