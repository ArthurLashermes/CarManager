using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.SerializeModels
{
    public class MaintenanceModelSerialize : ISerializeModelSerialize
    {
        public int VehicleId { get; set; }

        public int MileageAtMaintenance { get; set; }
        
        public string WorkDone { get; set; }

    }
}
