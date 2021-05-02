using AngelsAutomotive.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace AngelsAutomotive.Data.Repositories
{
    public interface IVehicleRepository : IGenericRepository<Vehicle>
    {

        IEnumerable<SelectListItem> GetComboVehicles();

    }
}
