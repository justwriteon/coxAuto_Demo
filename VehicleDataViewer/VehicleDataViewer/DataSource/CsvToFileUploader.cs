using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VehicleDataViewer.Models;

namespace VehicleDataViewer.DataSource
{
    /// <summary>
    /// Data file Uploader. Note: though name implies only CSV uploader file it still can be used for other files
    /// as long as it saves in file system but this is not the case with reading a file.
    /// </summary>
    public class CsvToFileUploader :  IDataUploader
    {
        private string appPath;
        public CsvToFileUploader()
        {
           // appPath = filePath;
            appPath = Startup._APPLICATION_PATH;
        }

        /// <summary>
        /// Uploads CSV file containg Vehicle sale data in the format provided by  Data Track
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UploadVehicleData(UploadViewModel model)
        {
            bool ret = true;
            if (model.FileToUpload != null)
            {
                try
                {
                   //File name and folder name has been hard coded for Demo
                    Guid gid = Guid.NewGuid();
                    string folderPath = Path.Combine(appPath, "VehicleData");
                    string filePath = Path.Combine(folderPath, "DefaultData.csv");
                    string oldFileName = Path.Combine(folderPath, $"DefaultData{gid}.csv");
                    if (File.Exists(filePath)) File.Move(filePath, oldFileName);
                    using (var fileStream = File.Create(filePath))
                    {
                        fileStream.Position = 0;
                         model.FileToUpload.CopyTo(fileStream);
                    }
                }
                catch
                {
                    ret = false;
                }
            }
            return ret;
        }
    }
}
