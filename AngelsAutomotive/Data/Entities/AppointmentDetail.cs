using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Entities
{
    public class AppointmentDetail : IEntity
    {
        public int Id { get; set; }     


        [Required]
        public Vehicle Vehicle { get; set; }


        public string LicencePlate { get; set; }

        public User User { get; set; }

    }
}
