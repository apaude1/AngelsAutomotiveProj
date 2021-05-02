using AngelsAutomotive.Data.Entities;
using AngelsAutomotive.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace AngelsAutomotive.Data.Repositories
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        private readonly DataContext _context;

        public VehicleRepository(DataContext context) : base(context)
        {
            _context = context;
        }



        public IEnumerable<SelectListItem> GetComboVehicles()
        {
            var model = new VehicleViewModel();
            var list = _context.Vehicles.Select(v => new SelectListItem 
            {
                Text = v.VehiclePlateNumber,
                Value = v.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem 
            {
                Text = "(Select a Vehicle...)",
                Value = "0"
            });

            return list;
        }
    }
}
