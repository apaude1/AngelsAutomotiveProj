using System.ComponentModel.DataAnnotations;

namespace AngelsAutomotive.Data.Entities
{
    public class Vehicle : IEntity
    {
      //  [Display(Name = "VCust_ID")]
     
        public int Id { get; set; }

        
        [Display(Name = "Vin Number")]
        [RegularExpression("[A-HJ-NPR-Z0-9]{13}[0-9]{4}", ErrorMessage = "Invalid Vehicle Identification Number Format.")]
        public string VinNummber { get; set; }

        //[Display(Name ="Image")]
        //public string ImageUrl { get; set; }
        [Display(Name = "Year")]
        public int VehYear { get; set; }

        [Display(Name = "Make")]
        public string VehMake { get; set; }

        [Display(Name = "Model")]
        public string VehModel { get; set; }

        [Display(Name = "Plate No")]
        [Required(ErrorMessage = "Please insert the {0}")]
        [RegularExpression(@"^(([A-Z]{2}-\d{2}-(\d{2}|[A-Z]{2}))|(\d{2}-(\d{2}-[A-Z]{2}|[A-Z]{2}-\d{2})))$", ErrorMessage = "Invalid licence plate format. Letters must be UpperCase.")]
        public string VehiclePlateNumber { get; set; }

        [Display(Name = "Mileage")]
        [Required(ErrorMessage = "Please insert the number of {0}")]
        [Range(0, int.MaxValue, ErrorMessage = "Only numbers are allowed.")]
        public int VehMileage { get; set; }


        //[Required(ErrorMessage = "Please insert the {0} of the vehicle.")]
        //[StringLength(50, ErrorMessage = "Minimum length of {2} characters.", MinimumLength = 3)]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed.")]
        //public string Color { get; set; }

        //[Required(ErrorMessage = "Please insert the correct type of {0}.")]
        //[StringLength(50, ErrorMessage = "Minimum length of {2} characters.", MinimumLength = 3)]
        //[RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only letters are allowed.")]
        //public string Fuel { get; set; }


        //[StringLength(150, ErrorMessage = "This field only accepts between {2} and {1} characteres long.", MinimumLength = 3)]
        //[DataType(DataType.MultilineText)]
        //public string Remarks { get; set; }

       // [Display(Name = "VCust_ID")]
        public User User { get; set; }


        //TODO: In principle this step not needed but when publishing on azure check this step
    }
}
