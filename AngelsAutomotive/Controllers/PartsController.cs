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
    public class PartsController : Controller
    {
        private readonly IPartsRepository _partsRepository;
        private readonly IUserHelper _userHelper;

        public PartsController(IPartsRepository partsRepository, IUserHelper userHelper)
        {
            _partsRepository = partsRepository;
            _userHelper = userHelper;
        }
        public IActionResult Index()
        {
            return View(_partsRepository.GetAll().OrderBy(p => p.Id));
        }

        // GET: Parts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parts = await _partsRepository.GetByIdAsync(id.Value);
            if (parts == null)
            {
                return NotFound();
            }

            return View(parts);
        }



        // GET: Parts/Create
        public IActionResult Create()
        {
            return View();
        }



        // POST: Parts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Parts parts)
        {
            if (ModelState.IsValid)
            {

                // parts.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);//TODO: Add Unique Index on Brand
                await _partsRepository.CreateAsync(parts);
                return RedirectToAction(nameof(Index));
            }
            return View(parts);
        }



        // GET: Parts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _partsRepository.GetByIdAsync(id.Value);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }



        // POST: Parts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Parts parts)
        {
            if (id != parts.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //parts.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await _partsRepository.UpdateAsync(parts);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _partsRepository.ExistAsync(parts.Id))
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
            return View(parts);
        }



        // GET: Parts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parts = await _partsRepository.GetByIdAsync(id.Value);
            if (parts == null)
            {
                return NotFound();
            }

            return View(parts);
        }



        // POST: Parts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parts = await _partsRepository.GetByIdAsync(id);
            await _partsRepository.DeleteAsync(parts);
            return RedirectToAction(nameof(Index));
        }


    }
}
