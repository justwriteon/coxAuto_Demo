using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleDataViewer.Models;

/// <summary>
/// Describes the actions performed by implementation classes
/// </summary>

namespace VehicleDataViewer.DataSource
{
    public interface IDataUploader
    {
        bool UploadVehicleData(UploadViewModel model);
    }
}
