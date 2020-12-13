using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleDataViewer.DataSource;

namespace VehicleDataViewer.Models
{
    public class DataRepository : IDataRepository
    {
        /// <summary>
        /// Generates Vehicle data service based on storage implementation injecting corresonding uploder
        /// TODO: Following code needs to be customized for expanding the functionality
        /// </summary>
        /// <param name="dataStore"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public IVehicleDataService GetDataService(VehicleDataStore dataStore )
        {
            IVehicleDataService ret;
            switch (dataStore)
            {
                case VehicleDataStore.DB_CSV:
                case VehicleDataStore.DB_XML:
                case VehicleDataStore.File_CSV:
                case VehicleDataStore.File_XML:
                default:
                    {
                        ret = new DataTrackVehicleService(new CsvToFileUploader());
                        break;
                    }
            }
                return ret;
        }
    }
}
