using System;
using System.ComponentModel.DataAnnotations;

namespace AngelsAutomotive.Models
{
    public class DeliverViewModel
    {
        public int Id { get; set; }



        [Display(Name = "Vehicle delivery date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime DeliveryDate { get; set; }
    }
}
