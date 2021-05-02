using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngelsAutomotive.Models
{
    public class AddItemViewModel
    {
        [Display(Name = "Vehicle")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a vehicle.")]
        public int VehicleId { get; set; }
       // public string VinNumber { get; set; }
        public IEnumerable<SelectListItem> Vehicles { get; set; }
    }
}
