using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Entities
{
    public class ServiceTypeOrder
    {
        public int ServiceTypeOrderId { get; set; }
        public  ServiceOrder ServiceOrder { get; set; }
        public ServiceType ServiceType { get; set; }

    }
}
