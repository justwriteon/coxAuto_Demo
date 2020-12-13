using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleDataViewer.Models;

namespace VehicleDataViewer.DataSource
{
    /// <summary>
    /// Defines list of operations available
    /// </summary>
   public  interface IVehicleDataService
    {

        /// <summary>
        /// Retrives Vehicle Data
        /// </summary>
        /// <returns></returns>
        IEnumerable<VehicleData> GetVehicleData();

        /// <summary>
        /// Retrives Most Sold Vehicle
        /// </summary>
        /// <returns></returns>
        string GetMostSoldVehicle();

        bool UploadVehicleData(UploadViewModel file);
    }
}
