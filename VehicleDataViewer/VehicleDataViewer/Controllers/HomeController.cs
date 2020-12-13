using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VehicleDataViewer.Models;
using VehicleDataViewer.DataSource;
using Microsoft.AspNetCore.Hosting;

namespace VehicleDataViewer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVehicleDataService _data;
       private readonly string _filePath;
        private readonly IDataRepository _repository;

        [Obsolete]
        public HomeController(ILogger<HomeController> logger, IDataRepository repository, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
           _filePath= hostingEnvironment.ContentRootPath;
            _data = repository.GetDataService(VehicleDataStore.File_CSV);
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Notes()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            string vName = string.Empty;
            var data = _repository.GetDataService(VehicleDataStore.File_CSV);
            vName = data.GetMostSoldVehicle();
            return View("Index",vName);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Error = string.Empty;
            return View();
        }

        [HttpPost]
        public IActionResult Create(UploadViewModel model)
        {
            string statusMessage = string.Empty;
            ViewBag.Error = string.Empty;
            if (ModelState.IsValid)
            {
               //TODO: Accept attribute not working. Need to verify and remove code below
                if(!model.FileToUpload.FileName.ToLower().EndsWith("csv"))
                {
                    // ViewBag.Error = "Only .CSV files allowed";
                    ViewBag.Error  = "Only .CSV files allowed !";
                    return View();
                }
                if (model.FileToUpload.Length == 0)
                {
                    ViewBag.Error = "Empty file Not allowed";
                    return View();
                }

                var data = _repository.GetDataService(VehicleDataStore.File_CSV);
                bool writeStatus = data.UploadVehicleData(model);
                statusMessage = writeStatus == true ? "File Successfully uploaded" : "There is error uploading file";
                return View("UploadData", statusMessage);
            }
            else
            {
                ViewBag.Error = string.Empty;
                return View();
            }
        }
        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ListData()
        {
            List<Object> ret = new List<object>();
            var data = _repository.GetDataService(VehicleDataStore.File_CSV);
            var model = data.GetVehicleData();
            model.ToList().ForEach(n => ret.Add(new
            {
                dealNumber = n.DealNumber,
                customerName = n.CustomerName,
                dealershipName = n.DealershipName,
                vehicle = n.Vehicle,
                price = $"CAD{n.Price:C}",
                saleDate = n.Date.ToString("M/d/yyyy")
            }));
            return Json(ret);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
