using AngelsAutomotive.Data.Entities;
using AngelsAutomotive.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Repositories
{
    public class ServiceTypeRepository : GenericRepository<ServiceType> ,IServiceTypeRepository
    {
        private readonly DataContext _context;

        public ServiceTypeRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetAllServiceTypes()
        {
            var model = new AddServiceItemViewModel();
            var list = _context.ServiceTypes.Select(s => new SelectListItem
            {
                Text = s.Type,
                Value = s.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a Service Type...)",
                Value = "0"
            });

            return list;
        }
    }
}
