using AngelsAutomotive.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Repositories
{
    public class PartsServiceOrderRepository : IPartsServiceOrderRepository
    {
        private readonly DataContext _context;

        public PartsServiceOrderRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<PartsServiceOrder> GetAllPartsServiceOrder => _context.PartsServiceOrders;
    }
}
