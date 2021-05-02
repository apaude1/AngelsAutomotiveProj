using AngelsAutomotive.Data.Repositories;
using AngelsAutomotive.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AngelsAutomotive.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public AppointmentsController(IAppointmentRepository appointmentRepository, IVehicleRepository vehicleRepository)
        {
            _appointmentRepository = appointmentRepository;
            _vehicleRepository = vehicleRepository;
        }



        public async Task<IActionResult> Index()
        {
            var model = await _appointmentRepository.GetAppointmentsAsync(this.User.Identity.Name);
            return View(model);
        }



        public async Task<IActionResult> Create()
        {
            var model = await _appointmentRepository.GetDetailTempsAsync(this.User.Identity.Name);
            return View(model);
        }

        public IActionResult AddVehicle()
        {
            var model = new AddItemViewModel
            {
                Vehicles = _vehicleRepository.GetComboVehicles(),
            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> AddVehicle(AddItemViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await _appointmentRepository.AddItemToAppointmentAsync(model, this.User.Identity.Name);
                return this.RedirectToAction("Create");
            }

            return View(model);
        }



        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _appointmentRepository.DeleteDetailTempAsync(id.Value);
            return this.RedirectToAction("Create");
        }



        public async Task<IActionResult> ConfirmAppointment()
        {
            var response = await _appointmentRepository.ConfirmAppointmentAsync(this.User.Identity.Name);
            if (response)
            {
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Create");
        }



        public async Task<IActionResult> Deliver(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var appointment = await _appointmentRepository.GetAppointmentsAsync(id.Value);
            if(appointment == null)
            {
                return NotFound();
            }

            var model = new DeliverViewModel
            {
                Id = appointment.Id,
                DeliveryDate = DateTime.Today
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Deliver(DeliverViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _appointmentRepository.DeliverAppointment(model);
                return RedirectToAction("Index");
            }

            return View();
        }


        //[HttpPost]      
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    await _appointmentRepository.DeleteAppointment(id.Value);
        //    return RedirectToAction("Index");
        //}

        // GET: serviceType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _appointmentRepository.GetByIdAsync(id.Value);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }


        // POST: serviceType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            await _appointmentRepository.DeleteDetailTempAsync(id);
            await _appointmentRepository.DeleteAsync(appointment);
            ViewBag.result = "Success";
            return RedirectToAction(nameof(Index));
        }
    }
}
