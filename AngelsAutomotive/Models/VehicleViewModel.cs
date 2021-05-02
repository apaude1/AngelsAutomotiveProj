using AngelsAutomotive.Data.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AngelsAutomotive.Models
{
    public class VehicleViewModel : Vehicle
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
