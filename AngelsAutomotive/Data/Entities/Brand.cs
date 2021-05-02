using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngelsAutomotive.Data.Entities
{
    public class Brand : IEntity
    {
        public int Id { get; set; }



        [Display(Name = "Brand")]
        [Required(ErrorMessage = "You must insert the {0}")]
        [MaxLength(50, ErrorMessage = "The field {0} can only contain {1} characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use only letters")]
        public string Name { get; set; } //TODO: add UniqueIndex for Brand Name on DataContext



        [Display(Name = "Model")]
        [MaxLength(50, ErrorMessage = "The field {0} can only contain {1} characters.")]
        [Required(ErrorMessage = "You must insert the {0}")]
        public string CarModel { get; set; } //TODO: add UniqueIndex for Car model on DataContext



        [Display(Name = "Cubic Capacity")]
        [MaxLength(10, ErrorMessage = "The field {0} can only contain {1} characters.")]
        [Required(ErrorMessage = "You must insert the {0}")]
        public string EngineCapacity { get; set; }



        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "You must insert the {0}")]
        [Display(Name = "Year Model")]
        public DateTime ModelYear { get; set; }



        public User User { get; set; }



        //public ICollection<Vehicle> Vehicles { get; set; }//will receive a collection of vehicles
    }
}
