using AngelsAutomotive.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Models
{
    public class AddServiceItemViewModel
    {
        [Display(Name = "Vehicle")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a vehicle.")]
        public int VehicleId { get; set; }

        //public string VinNumber { get; set; }


        public IEnumerable<SelectListItem> Vehicles { get; set; }
        [Display(Name = "Service Type")]
       // public int ServiceTypeId { get; set; }
        public IEnumerable<SelectListItem> ServiceType { get; set; }
        [Display(Name = "Parts")]
       // public int PartsId { get; set; }
        public IEnumerable<SelectListItem> Parts { get; set; }
        public string Confirm { get; set; }

        public User User { get; set; }
    }
}
