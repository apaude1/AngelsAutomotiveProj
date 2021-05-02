using System.ComponentModel.DataAnnotations;

namespace AngelsAutomotive.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]//the username will be an email
        public string Username { get; set; }


        [Required]
        [MinLength(6)]//change this when we move to production
        public string Password { get; set; }


        public bool RememberMe { get; set; }
    }
}
