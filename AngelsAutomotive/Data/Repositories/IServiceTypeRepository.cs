using AngelsAutomotive.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Repositories
{
   public interface IServiceTypeRepository : IGenericRepository<ServiceType>
    {
        IEnumerable<SelectListItem> GetAllServiceTypes();
    }
}
