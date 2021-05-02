using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngelsAutomotive.Data.Entities
{
    public class State : IEntity
    {
        public int Id { get; set; }



        [Required]
        [MaxLength(50, ErrorMessage ="The field {0} can only contain {1} characters.")]
        public string Name { get; set; }



        public ICollection<City> Cities { get; set; }


        [Display(Name = "Number of Cities")]
        public int NumberCities { get { return this.Cities == null ? 0 : this.Cities.Count; } }
    }
}
