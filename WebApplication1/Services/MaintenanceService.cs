    using Server.Domain;
    using WebApplication1;

    namespace Server.Services
    {
        public class MaintenanceService
        {
            private readonly ApplicationDbContext _context;


            public MaintenanceService(ApplicationDbContext context)
            {
                _context = context;
            }

            /// <summary>
            /// Méthode pour calculer le retard d'entretien
            /// </summary>
            /// <returns></returns>
            /// <exception cref="InvalidOperationException"></exception>
            public int CalculateMaintenanceDelay(Maintenance maintenance)
            {
                var vehicle = maintenance.Vehicle;
                if (vehicle == null || vehicle.Car == null)
                    throw new InvalidOperationException("Les informations du véhicule ou du modèle de voiture sont manquantes.");

                return maintenance.MileageAtMaintenance + vehicle.Car.MaintenanceFrequency - vehicle.Mileage;
            }
            public void ValidateMileageAtMaintenance(Maintenance maintenance)
            {
                var vehicle = maintenance.Vehicle;
                if (vehicle == null)
                {
                    var vehiclesRepository = _context.Set<Vehicle>();
                    vehicle = vehiclesRepository
                        .FirstOrDefault(x => x.Id == maintenance.VehicleId);
                }
                if (maintenance.MileageAtMaintenance != vehicle?.Mileage)
                    throw new ArgumentException("Lors de l’ajout de travaux, le kilométrage doit être le kilométrage courant du véhicule.");
            }

        }
    }
