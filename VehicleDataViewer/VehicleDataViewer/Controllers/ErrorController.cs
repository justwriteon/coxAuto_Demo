using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace VehicleDataViewer.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ErrorController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //Used default error log for demo

       [Route("Error")]
       [AllowAnonymous]
        public IActionResult Error(int statusCode)
        {
            var errorDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError(errorDetails.Error.Message);
            _logger.LogError(errorDetails.Path);
            _logger.LogError(errorDetails.Error.StackTrace);
            ViewBag.Message = errorDetails.Error.Message;
            return View("CustomError");
        }
    }
}
