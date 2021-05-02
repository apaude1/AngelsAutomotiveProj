using AngelsAutomotive.Data.Entities;
using AngelsAutomotive.Helpers;
using AngelsAutomotive.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public AppointmentRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }



        public async Task<IQueryable<Appointment>> GetAppointmentsAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                return _context.Appointments
                    .Include(a => a.User)
                    .Include(a => a.Details)
                    .ThenInclude(d => d.Vehicle)
                    .OrderByDescending(o => o.AppointmentDate);
            }

            return _context.Appointments
                .Include(a => a.Details)
                .ThenInclude(d => d.Vehicle)
                .Where(a => a.User == user)
                .OrderByDescending(o => o.AppointmentDate);
        }



        public async Task<IQueryable<AppointmentDetailTemp>> GetDetailTempsAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return null;
            }

            return _context.AppointmentDetailsTemp
                .Include(a => a.Vehicle)
                .Where(a => a.User == user)
                .OrderBy(o => o.Vehicle.VehiclePlateNumber);
        }


        public async Task AddItemToAppointmentAsync(AddItemViewModel model, string userName)
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

            var appointmentDetailTemp = await _context.AppointmentDetailsTemp
                .Where(adt => adt.User == user && adt.Vehicle == vehicle)
                .FirstOrDefaultAsync();

            if (appointmentDetailTemp == null)
            {
                appointmentDetailTemp = new AppointmentDetailTemp
                {
                    LicencePlate = vehicle.VehiclePlateNumber,
                    Vehicle = vehicle,
                    User = user
                };

                _context.AppointmentDetailsTemp.Add(appointmentDetailTemp);
            }
            else
            {
                _context.AppointmentDetailsTemp.Update(appointmentDetailTemp);
            }

            await _context.SaveChangesAsync();
        }



        public async Task DeleteDetailTempAsync(int id)
        {
            var appointmentDetailTemp = await _context.AppointmentDetails.FindAsync(id);
            if(appointmentDetailTemp == null)
            {
                return;
            }

            _context.AppointmentDetails.Remove(appointmentDetailTemp);
            await _context.SaveChangesAsync();
        }



        public async Task<bool> ConfirmAppointmentAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if(user == null)
            {
                return false;
            }

            var appointmentTemps = await _context.AppointmentDetailsTemp
                .Include(a => a.Vehicle)
                .Where(a => a.User == user)
                .ToListAsync();

            if (appointmentTemps == null || appointmentTemps.Count == 0)
            {
                return false;
            }


            var details = appointmentTemps.Select(a => new AppointmentDetail
            {
                LicencePlate = a.LicencePlate,
                Vehicle = a.Vehicle
            }).ToList();


            var appointment = new Appointment
            {
                AppointmentDate = DateTime.UtcNow,
                User = user,
                Details = details,
            };

            _context.Appointments.Add(appointment);
            _context.AppointmentDetailsTemp.RemoveRange(appointmentTemps);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeliverAppointment(DeliverViewModel model)
        {
            var appointment = await _context.Appointments.FindAsync(model.Id);
            if (appointment == null)
            {
                return;
            }

            appointment.DeliveryDate = model.DeliveryDate;
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task<Appointment> GetAppointmentsAsync(int id)
        {
            return await _context.Appointments.FindAsync(id);
        }


        public async Task DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment== null)
            {
                return;
            }

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }

    }
}
