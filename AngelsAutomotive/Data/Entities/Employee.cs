using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Entities
{
    public class Employee : IEntity
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string MName { get; set; }
        public string LName { get; set; }
        public int StreetNumber { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int Zipcode { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }

    }
}
