using System.ComponentModel.DataAnnotations;

namespace AngelsAutomotive.Data.Entities
{
    public class AppointmentDetailTemp : IEntity
    {
        public int Id { get; set; }



        [Required]
        public User User { get; set; }



        [Required]
        public Vehicle Vehicle { get; set; }


        public string LicencePlate { get; set; }
    }
}
