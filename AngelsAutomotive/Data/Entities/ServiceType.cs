using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Entities
{
    public class ServiceType : IEntity
    {
        [Display(Name = "Service Type ID")]
        public int Id { get; set; }

        [Display(Name = "Service Type")]
        public string Type { get; set; }

        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public Double Price { get; set; }

    }
}
