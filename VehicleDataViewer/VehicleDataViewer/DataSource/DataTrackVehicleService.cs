using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using VehicleDataViewer.Utilities;
using VehicleDataViewer.Models;
using System.Text;

namespace VehicleDataViewer.DataSource
{
    public class DataTrackVehicleService : IVehicleDataService
    {
        IEnumerable<VehicleData> _vehicleData = new List<VehicleData>();
        private readonly IDataUploader _uploader;
        private readonly string appPath;

        public DataTrackVehicleService(IDataUploader uploader )
        {
            _uploader = uploader;
            appPath = Startup._APPLICATION_PATH;
        }
        /// <summary>
        /// Evaluates most sold vehicle within available data
        /// </summary>
        /// <returns></returns>

        public string GetMostSoldVehicle()
        {
            string ret = string.Empty;
            try
            {
                if (_vehicleData.Count() == 0)
                {
                    _vehicleData = LoadFromCSVFile();
                }
                if (_vehicleData.Count() > 0)
                    ret = _vehicleData.GroupBy(v => v.Vehicle)
                    .Select(g => new { Vehicle = g.Key, Count = g.Count() })
                    .OrderByDescending(o => o.Count).First().Vehicle;
                else
                {
                    ret = "No Data available!";
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        /// <summary>
        /// Returns Vehicle sale data
        /// </summary>
        /// <returns></returns>
        public IEnumerable<VehicleData> GetVehicleData()
        {
            try
            {
                if (_vehicleData.Count() == 0)
                {
                    _vehicleData = LoadFromCSVFile();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _vehicleData;
        }

        /// <summary>
        /// Uploads CSV file containg Vehicle data
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
       public bool UploadVehicleData(UploadViewModel file)
        {
            return   _uploader.UploadVehicleData(file);
        }

        /// <summary>
        /// Loads data from CSV file. Note this is specific to the data format provided by Data Track
        /// </summary>
        /// <returns></returns>
        private IEnumerable<VehicleData> LoadFromCSVFile()
        {
            List<VehicleData> ret = new List<VehicleData>();
            try
            {
                Regex regx = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                string folderPath = Path.Combine(appPath, "VehicleData");
                string filePath = Path.Combine(folderPath, "DefaultData.csv");
                //Since it's demo purpose file name has been hard coded.
                var arrLines = File.ReadAllLines(filePath, Encoding.UTF7);
                if (arrLines.Length > 1)
                {
                    bool headerLine = true;
                    foreach(string sLine in arrLines)
                    {
                       if(!headerLine)
                        {
                            var arrRecord = regx.Split(sLine);
                            if (arrRecord.Length == 6)
                            {
                                var data = GetVehicleDataFromArray(arrRecord);
                                if (data != null)
                                {
                                    ret.Add(data);
                                }
                            }
                        }
                        headerLine = false;
                    }
            }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        /// <summary>
        /// Loads data from CSV file. Note this is specific to the data format provided by Data Track
        /// Multiple reads results in performance penalty hence omitted
        /// </summary>
        /// <returns></returns>
        private IEnumerable<VehicleData> LoadFromCSVFileLineByLine()
        {
            List<VehicleData> ret = new List<VehicleData>();
            try
            {
                string sLine = null;
                Regex regx = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                string folderPath = Path.Combine(appPath, "VehicleData");
                string filePath = Path.Combine(folderPath, "DefaultData.csv");
                //Since it's demo purpose file name has been hard coded.
                using (StreamReader sr = new StreamReader(filePath,Encoding.UTF7))
                {
                    bool headerRow = true;
                    while ((sLine = sr.ReadLine()) != null)
                    {
                        if (!headerRow)
                        {
                            var arrRecord = regx.Split(sLine);
                            var data = GetVehicleDataFromArray(arrRecord);
                            if (data != null)
                            {
                                ret.Add(data);
                            }
                        }
                        headerRow = false;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return ret;
        }

        private VehicleData GetVehicleDataFromArray(string[] arrRec)
        {
            VehicleData ret = null;
            try
            {
                int.TryParse(arrRec[0], out int dealNo);
                var price = arrRec[4].GetPriceDecimal();
                DateTime  dt = arrRec[5].GetFormatedDate("M/d/yyyy");
                ret = new VehicleData
                {
                    DealNumber = dealNo,
                    CustomerName = arrRec[1],
                    DealershipName = arrRec[2],
                    Vehicle = arrRec[3],
                    Price = price,
                    Date = dt
                };
            }
            catch(Exception ex)
            {
               //Exception is NOT thrown in order to ignore dirty data and continue.....
                //Log Exception details
                //can be used to count number of failed records to log.
                string LogMessage = $"Error loading Record: {string.Join(',',arrRec)} Error : {ex.Message}";
            }
            return ret;
        }

    }
}


