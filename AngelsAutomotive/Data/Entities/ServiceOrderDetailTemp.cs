using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Entities
{
    public class ServiceOrderDetailTemp
    {
        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Vehicle Vehicle { get; set; }

        public ServiceType serviceType { get; set; }

        public Parts parts { get; set; }

        public string LicencePlate { get; set; }
        public string ServiceType { get; set; }
        public string PartName { get; set; }
    }
}
