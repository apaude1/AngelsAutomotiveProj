using System.ComponentModel.DataAnnotations;

namespace AngelsAutomotive.Models
{
    public class CityViewModel
    {
        public int stateId { get; set; }



        public int CityId { get; set; }


        [Required]
        [Display(Name = "City")]
        [MaxLength(50, ErrorMessage = "The field {0} can only contain {1} characters.")]
        public string Name { get; set; }
    }
}
