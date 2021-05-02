using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Entities
{
    public class PartsServiceOrder
    {
       
        public int PartsServiceOrderId { get; set; }

        [Display(Name = "Parts_Number")]
        public Parts Parts { get; set; }

        [Display(Name = "Service_Order_Num")]
        public  ServiceOrder ServiceOrder { get; set; }
        public int Quantity { get; set; }

    }
}
