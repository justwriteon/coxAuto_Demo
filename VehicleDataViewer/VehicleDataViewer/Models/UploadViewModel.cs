using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleDataViewer.Models
{
    public class UploadViewModel
    {
        [Required(ErrorMessage ="Please choose file to upload!")]
        [DisplayName("Upload File")]
        public IFormFile FileToUpload { get; set; }
    }
}
