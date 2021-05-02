﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngelsAutomotive.Models
{
    public class ChangeUserViewModel //makes changes to the user data, the password change is in another viewModel -> after the ViewModels we have to go to the UserHelper to make these changes)
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }



        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }



        [MaxLength(100, ErrorMessage = "The field {0} can only contain {1} characters.")]
        public string Address { get; set; }



        [MaxLength(20, ErrorMessage = "The field {0} can only contain {1} characters.")]
        public string PhoneNumber { get; set; }



        [Display(Name = "City")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a city")]
        public int CityId { get; set; }



        public IEnumerable<SelectListItem> Cities { get; set; }



        [Display(Name = "State")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a State")]
        public int stateId { get; set; }



        public IEnumerable<SelectListItem> States { get; set; }
    }
}
