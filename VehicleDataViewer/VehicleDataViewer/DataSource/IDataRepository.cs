using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleDataViewer.Models;

namespace VehicleDataViewer.DataSource
{
   public interface IDataRepository
    {
        IVehicleDataService GetDataService(VehicleDataStore dataStore);
    }
}
