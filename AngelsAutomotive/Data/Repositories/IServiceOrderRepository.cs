using AngelsAutomotive.Data.Entities;
using AngelsAutomotive.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Repositories
{
    public interface IServiceOrderRepository : IGenericRepository<ServiceOrder>
    {
        IEnumerable<ServiceOrder> GetAllServiceOrders { get; }

        Task<IQueryable<ServiceOrder>> GetServiceOrderAsync(string userName);

        Task<IQueryable<ServiceOrderDetailTemp>> GetServiceOrderDetailTempsAsync(string userName);

        Task AddItemToServiceOrderAsync(AddServiceItemViewModel model, string userName);

        Task DeleteServiceOrderDetailTempAsync(int id);
        Task<bool> ConfirmServiceOrderAsync(string userName);


        //Task DeliverAppointment(DeliverViewModel model);

        Task<ServiceOrder> GetServiceOrderAsync(int id);

        Task DeleteServiceOrder(int id);
    }
}
