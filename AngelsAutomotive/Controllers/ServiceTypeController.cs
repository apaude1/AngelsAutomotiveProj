using AngelsAutomotive.Data.Entities;
using AngelsAutomotive.Data.Repositories;
using AngelsAutomotive.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Controllers
{
    public class ServiceTypeController : Controller
    {
        private readonly IServiceTypeRepository _serviceTypeRepository;
        private readonly IUserHelper _userHelper;

        public ServiceTypeController(IServiceTypeRepository serviceTypeRepository, IUserHelper userHelper)
        {
            _serviceTypeRepository = serviceTypeRepository;
            _userHelper = userHelper;
        }
        public IActionResult Index()
        {
            return View(_serviceTypeRepository.GetAll().OrderBy(s => s.Id));
        }


        // GET: serviceType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceType = await _serviceTypeRepository.GetByIdAsync(id.Value);
            if (serviceType == null)
            {
                return NotFound();
            }

            return View(serviceType);
        }

        // GET: serviceType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: serviceType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {

                //serviceType.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);//TODO: Add Unique Index on ServiceType
                await _serviceTypeRepository.CreateAsync(serviceType);
                return RedirectToAction(nameof(Index));
            }
            return View(serviceType);
        }


        // GET: serviceType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _serviceTypeRepository.GetByIdAsync(id.Value);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        // POST: serviceType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceType servicetype)
        {
            if (id != servicetype.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   // servicetype.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await _serviceTypeRepository.UpdateAsync(servicetype);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _serviceTypeRepository.ExistAsync(servicetype.Id))//!ServiceTypeExists(ServiceType.Id) < -original code
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(servicetype);
        }


        // GET: serviceType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicetype = await _serviceTypeRepository.GetByIdAsync(id.Value);
            if (servicetype == null)
            {
                return NotFound();
            }

            return View(servicetype);
        }


        // POST: serviceType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servicetype = await _serviceTypeRepository.GetByIdAsync(id);
            await _serviceTypeRepository.DeleteAsync(servicetype);
            return RedirectToAction(nameof(Index));
        }

    }
}
