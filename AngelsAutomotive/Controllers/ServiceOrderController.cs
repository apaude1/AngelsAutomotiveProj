using AngelsAutomotive.Data.Repositories;
using AngelsAutomotive.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Controllers
{
    [Authorize]
    public class ServiceOrderController : Controller
    {
        private readonly IServiceOrderRepository _serviceOrderRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IPartsRepository _partsRepository;

        public ServiceOrderController (IServiceOrderRepository serviceOrderRepository, IVehicleRepository vehicleRepository,IServiceTypeRepository serviceTypeRepository, IPartsRepository partsRepository)
        {
            _serviceOrderRepository = serviceOrderRepository;
            _vehicleRepository = vehicleRepository;
            _serviceTypeRepository = serviceTypeRepository;
            _partsRepository = partsRepository;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _serviceOrderRepository.GetServiceOrderAsync(this.User.Identity.Name);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _serviceOrderRepository.GetServiceOrderDetailTempsAsync(this.User.Identity.Name);
            return View(model);
        }



        public IActionResult AddVehicle()
        {
            var model = new AddServiceItemViewModel
            {
                Vehicles = _vehicleRepository.GetComboVehicles(),
                ServiceType = _serviceTypeRepository.GetAllServiceTypes(),
                Parts = _partsRepository.GetAllParts()
            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> AddVehicle(AddServiceItemViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await _serviceOrderRepository.AddItemToServiceOrderAsync(model, this.User.Identity.Name);
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

            await _serviceOrderRepository.DeleteServiceOrderDetailTempAsync(id.Value);
            return this.RedirectToAction("Create");
        }



        public async Task<IActionResult> ConfirmAppointment()
        {
            var response = await _serviceOrderRepository.ConfirmServiceOrderAsync(this.User.Identity.Name);
            if (response)
            {
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Create");
        }



        //public async Task<IActionResult> Deliver(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var appointment = await _serviceOrderRepository.GetServiceOrderAsync(id.Value);
        //    if (appointment == null)
        //    {
        //        return NotFound();
        //    }

        //    var model = new DeliverViewModel
        //    {
        //        Id = appointment.Id,
        //        DeliveryDate = DateTime.Today
        //    };

        //    return View(model);
        //}


        //[HttpPost]
        //public async Task<IActionResult> Deliver(DeliverViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _serviceOrderRepository.DeliverAppointment(model);
        //        return RedirectToAction("Index");
        //    }

        //    return View();
        //}




        [HttpPost]      
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _serviceOrderRepository.DeleteServiceOrder(id.Value);
            return RedirectToAction("Index");
        }

    }
}
