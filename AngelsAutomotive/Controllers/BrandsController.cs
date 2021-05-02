using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AngelsAutomotive.Data;
using AngelsAutomotive.Data.Entities;
using AngelsAutomotive.Helpers;
using AngelsAutomotive.Data.Repositories;

namespace AngelsAutomotive.Controllers
{
    public class BrandsController : Controller
    {
        //private readonly DataContext _context;
        private readonly IBrandRepository _brandRepository;
        private readonly IUserHelper _userHelper;

        public BrandsController(/*DataContext context,*/ IBrandRepository brandRepository, IUserHelper userHelper)
        {
            //_context = context;
            _brandRepository = brandRepository;
            _userHelper = userHelper;
        }



        // GET: Brands
        public IActionResult Index()
        {
            return View(_brandRepository.GetAll().OrderBy(b => b.Name));
        }



        // GET: Brands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _brandRepository.GetByIdAsync(id.Value);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }



        // GET: Brands/Create
        public IActionResult Create()
        {
            return View();
        }



        // POST: Brands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                
                brand.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);//TODO: Add Unique Index on Brand
                await _brandRepository.CreateAsync(brand);
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }



        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _brandRepository.GetByIdAsync(id.Value);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }



        // POST: Brands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Brand brand)
        {
            if (id != brand.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {                 
                    brand.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await _brandRepository.UpdateAsync(brand);                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _brandRepository.ExistAsync(brand.Id))//! BrandExists (brand.Id) <- original code
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
            return View(brand);
        }



        // GET: Brands/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brand = await _brandRepository.GetByIdAsync(id.Value);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }



        // POST: Brands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            await _brandRepository.DeleteAsync(brand);
            return RedirectToAction(nameof(Index));
        }



       /*private bool BrandExists(int id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }*/
    }
}
