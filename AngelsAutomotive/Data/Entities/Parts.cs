using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Entities
{
    public class Parts : IEntity
    {
        public int Id { get; set; }

        public string PartName { get; set; }

        public string PartSeller { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public Double ResellingPrice { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}", ApplyFormatInEditMode = true)]
        public Double PurchasedPrice { get; set; }

        //public PartsServiceOrder PartServiceOder { get; set; }

        //public ServiceTypeOrder serviceTypeOrder { get; set; }

    }
}
