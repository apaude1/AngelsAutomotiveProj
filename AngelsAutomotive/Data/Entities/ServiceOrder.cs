using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Entities
{
    public class ServiceOrder : IEntity
    {
        [Display(Name = "Service_Order_ID")]
        public int Id { get; set; }

        public DateTime DateTime {get; set;}

        [Display(Name = "VIN")]
        public string VinNummber { get; set; }

        [Display(Name = "EMP_ID")]
        public User User { get; set; }
        //public  Vehicle Vehicle { get; set; }
        // public  PartsServiceOrder PartsServiceOrder { get; set; }
        // public  ServiceTypeOrder ServiceTypeOrder { get; set; }

        public IEnumerable<ServiceOrderDetails> ServiceOrderDetails { get; set; }//returns a list of details from ServiceOrderDetails.cs
    }
}
