using AngelsAutomotive.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Repositories
{
    public class ServiceTypeOrderRepository : IServiceTypeOrderRepository
    {
        private readonly DataContext _context;

        public ServiceTypeOrderRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<ServiceTypeOrder> GetAllServiceTypeOrder => _context.ServiceTypeOrders;
    }
}
