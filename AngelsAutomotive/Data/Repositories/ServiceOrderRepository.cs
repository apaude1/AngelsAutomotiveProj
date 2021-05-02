using AngelsAutomotive.Data.Entities;
using AngelsAutomotive.Helpers;
using AngelsAutomotive.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Repositories
{
    public class ServiceOrderRepository : GenericRepository<ServiceOrder>, IServiceOrderRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        public ServiceOrderRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public IEnumerable<ServiceOrder> GetAllServiceOrders => _context.ServiceOrders;

        public async Task<IQueryable<ServiceOrder>> GetServiceOrderAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                return _context.ServiceOrders
                    .Include(a => a.User)
                    .Include(a => a.ServiceOrderDetails)
                    .ThenInclude(d => d.Vehicle)
                    .OrderByDescending(o => o.DateTime);
            }

            return _context.ServiceOrders
                .Include(a => a.ServiceOrderDetails)
                .ThenInclude(d => d.Vehicle)
                .Where(a => a.User == user)
                .OrderByDescending(o => o.DateTime);
        }



        public async Task<IQueryable<ServiceOrderDetailTemp>> GetServiceOrderDetailTempsAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            return _context.ServiceOrderDetailTemp
                .Include(a => a.Vehicle)
                .Where(a => a.User == user)
                .OrderBy(o => o.Vehicle.VehiclePlateNumber);
        }



        public async Task AddItemToServiceOrderAsync(AddServiceItemViewModel model, string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return;
            }

            var vehicle = await _context.Vehicles.FindAsync(model.VehicleId);
            if (vehicle == null)
            {
                return;
            }


            var serviceType = await _context.ServiceTypes.FindAsync(model.VehicleId);
            if (serviceType == null)
            {
                return;
            }

            var parts = await _context.Parts.FindAsync(model.VehicleId);
            if (serviceType == null)
            {
                return;
            }

            var serviceOrderDetailTemp = await _context.ServiceOrderDetailTemp
                .Where(adt => adt.User == user && adt.Vehicle == vehicle)
                .FirstOrDefaultAsync();

            if (serviceOrderDetailTemp == null)
            {
                serviceOrderDetailTemp = new ServiceOrderDetailTemp
                {
                    LicencePlate = vehicle.VehiclePlateNumber,
                    ServiceType = serviceType.Type,
                    PartName = parts.PartName,
                    Vehicle = vehicle,
                    User = user
                };

                _context.ServiceOrderDetailTemp.Add(serviceOrderDetailTemp);
            }
            else
            {
                _context.ServiceOrderDetailTemp.Update(serviceOrderDetailTemp);
            }

            await _context.SaveChangesAsync();
        }



        public async Task DeleteServiceOrderDetailTempAsync(int id)
        {
            var serviceOrderDetailTemp = await _context.ServiceOrderDetailTemp.FindAsync(id);
            if (serviceOrderDetailTemp == null)
            {
                return;
            }

            _context.ServiceOrderDetailTemp.Remove(serviceOrderDetailTemp);
            await _context.SaveChangesAsync();
        }



        public async Task<bool> ConfirmServiceOrderAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return false;
            }

            var serviceordertemps = await _context.ServiceOrderDetailTemp
                .Include(a => a.Vehicle)
                .Where(a => a.User == user)
                .ToListAsync();

            if (serviceordertemps == null || serviceordertemps.Count == 0)
            {
                return false;
            }


            var details = serviceordertemps.Select(a => new ServiceOrderDetails
            {
                LicencePlate = a.LicencePlate,
                Vehicle = a.Vehicle
            }).ToList();


            var serviceorder = new ServiceOrder
            {
                DateTime = DateTime.UtcNow,
                User = user,
                ServiceOrderDetails = details,
            };

            _context.ServiceOrders.Add(serviceorder);
            _context.ServiceOrderDetailTemp.RemoveRange(serviceordertemps);
            await _context.SaveChangesAsync();
            return true;
        }

        //public async Task DeliverAppointment(DeliverViewModel model)
        //{
        //    var appointment = await _context.Appointments.FindAsync(model.Id);
        //    if (appointment == null)
        //    {
        //        return;
        //    }

        //    appointment.DeliveryDate = model.DeliveryDate;
        //    _context.Appointments.Update(appointment);
        //    await _context.SaveChangesAsync();
        //}

        public async Task<ServiceOrder> GetServiceOrderAsync(int id)
        {
            return await _context.ServiceOrders.FindAsync(id);
        }


        public async Task DeleteServiceOrder(int id)
         {
             var serviceorder = await _context.ServiceOrderDetails.FindAsync(id);
             if (serviceorder == null)
             {
                 return;
             }

             _context.ServiceOrderDetails.Remove(serviceorder);
             await _context.SaveChangesAsync();
         }


    }
}
