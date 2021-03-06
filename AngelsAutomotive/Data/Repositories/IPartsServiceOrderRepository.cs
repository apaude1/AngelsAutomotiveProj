using AngelsAutomotive.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Repositories
{
    interface IPartsServiceOrderRepository
    {
        IEnumerable<PartsServiceOrder> GetAllPartsServiceOrder { get; }
    }
}
