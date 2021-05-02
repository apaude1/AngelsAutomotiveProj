using AngelsAutomotive.Data.Entities;
using AngelsAutomotive.Models;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Repositories
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IQueryable<Appointment>> GetAppointmentsAsync(string userName);



        Task<IQueryable<AppointmentDetailTemp>> GetDetailTempsAsync(string userName);



        Task AddItemToAppointmentAsync(AddItemViewModel model, string userName);

        Task DeleteDetailTempAsync(int id);


        Task<bool> ConfirmAppointmentAsync(string userName);


        Task DeliverAppointment(DeliverViewModel model);


        Task<Appointment> GetAppointmentsAsync(int id);


       Task DeleteAppointment(int id);
    }
}
