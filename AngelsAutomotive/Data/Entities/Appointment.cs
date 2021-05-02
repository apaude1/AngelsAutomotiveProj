using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngelsAutomotive.Data.Entities
{
    public class Appointment : IEntity
    {
        [Display(Name = "Appointment_ID")]
        public int Id { get; set; }



        [Required]
        [Display(Name = "Appointment date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]       
        public DateTime AppointmentDate { get; set; }


     
        [Display(Name = "Vehicle delivery date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]    
        public DateTime? DeliveryDate { get; set; }      


        [Required]
        [Display(Name = "Customer_ID")]
        public User User { get; set; }

        public IEnumerable<AppointmentDetail> Details { get; set; }//returns a list of details from AppointmentDetail.cs



        [Display(Name = "Appointment date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime? AppointmentDateLocal
        {
            get
            {
                if (this.AppointmentDate == null)
                {
                    return null;
                }

                return this.AppointmentDate.ToLocalTime();
            }
        }

    }
}
