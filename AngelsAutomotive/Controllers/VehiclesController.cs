using AngelsAutomotive.Data.Repositories;
using AngelsAutomotive.Helpers;
using AngelsAutomotive.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AngelsAutomotive.Controllers
{
    [Authorize]
    public class VehiclesController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUserHelper _userHelper;
        private readonly IImageHelper _imageHelper;//attribute
        private readonly IConverterHelper _converterHelper;

        public VehiclesController(
            IVehicleRepository vehicleRepository,
            IUserHelper userHelper,
            IImageHelper imageHelper,
            IConverterHelper converterHelper)//parameter
        {
            _vehicleRepository = vehicleRepository;
            _userHelper = userHelper;
            _imageHelper = imageHelper;//assignment
            _converterHelper = converterHelper;
        }



        // GET: Vehicles
        public IActionResult Index()
        {
          
             return View(_vehicleRepository.GetAll());
            //return View(_vehicleRepository.GetByIdAsync();
        }



        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VehicleNotFound");
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);
            if (vehicle == null)
            {
                return new NotFoundViewResult("VehicleNotFound");
            }

            return View(vehicle);
        }



        // GET: Vehicles/Create
        /* [Authorize(Roles ="Admin")] *///Just let the admin create vehicles
        public IActionResult Create()
        {
            return View();
        }



        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var path = string.Empty;

                    if (model.ImageFile != null)
                    {
                        path = await _imageHelper.UploadImageAsync(model.ImageFile, "Vehicles");
                    }

                    var vehicle = _converterHelper.ToVehicle(model, path, true);

                    //TODO: Change for the logged user
                    vehicle.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);//the name is the username which in this case is an email
                    await _vehicleRepository.CreateAsync(vehicle);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There is already a Licence Plate with that number.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }                
            }
            return View(model);
        }

        // GET: Vehicles/Edit/5
       /* [Authorize(Roles = "Admin")]*/
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VehicleNotFound");
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);
            if (vehicle == null)
            {
                return new NotFoundViewResult("VehicleNotFound");
            }

            var model = _converterHelper.ToVehicleViewModel(vehicle);

            return View(model);
        }



        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VehicleViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    try
                    {
                        //var path = model.ImageUrl;//Get the path that exists in Sql

                        //if (model.ImageFile != null && model.ImageFile.Length > 0)
                        //{
                        //    path = await _imageHelper.UploadImageAsync(model.ImageFile, "Vehicles");
                        //}

                        var vehicle = _converterHelper.ToVehicleViewModel(model);

                        //TODO: Change for the logged user
                        vehicle.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                        await _vehicleRepository.UpdateAsync(vehicle);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!await _vehicleRepository.ExistAsync(model.Id))
                        {
                            return new NotFoundViewResult("VehicleNotFound");
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There is already a Licence Plate with that number.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }                
            }
            return View(model);
        }



        // GET: Vehicles/Delete/5
        /*[Authorize(Roles = "Admin")]*/
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new NotFoundViewResult("VehicleNotFound");
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);
            if (vehicle == null)
            {
                return new NotFoundViewResult("VehicleNotFound");
            }

            return View(vehicle);
        }



        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            await _vehicleRepository.DeleteAsync(vehicle);

            return RedirectToAction(nameof(Index));
        }



        public IActionResult VehicleNotFound()
        {
            return View();
        }
    }
}
