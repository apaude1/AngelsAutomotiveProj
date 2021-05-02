using System.ComponentModel.DataAnnotations;

namespace AngelsAutomotive.Models
{
    public class ChangePasswordViewModel //change user password
    {
        [Required]
        public string OldPassword { get; set; }



        [Required]
        public string NewPassword { get; set; }



        [Required]
        [Compare("NewPassword")]
        public string Confirm { get; set; }
    }
}
