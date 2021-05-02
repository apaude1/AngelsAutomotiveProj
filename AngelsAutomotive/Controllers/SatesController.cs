using AngelsAutomotive.Data.Entities;
using AngelsAutomotive.Data.Repositories;
using AngelsAutomotive.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AngelsAutomotive.Controllers
{
    public class SatesController : Controller
    {
        private readonly IStateRepository _stateRepository;

        public SatesController(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }


        public async Task<IActionResult> DeleteCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _stateRepository.GetCityAsync(id.Value);
            if (city == null)
            {
                return NotFound();
            }

            var stateId = await _stateRepository.DeleteCityAsync(city);
            return this.RedirectToAction($"Details/{stateId}");
        }


        public async Task<IActionResult> EditCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var city = await _stateRepository.GetCityAsync(id.Value);
            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }


        [HttpPost]
        public async Task<IActionResult> EditCity(City city)
        {
            if (this.ModelState.IsValid)
            {
                var stateId = await _stateRepository.UpdateCityAsync(city);
                if (stateId != 0)
                {
                    return this.RedirectToAction($"Details/{stateId}");
                }
            }

            return this.View(city);
        }


        public async Task<IActionResult> AddCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var State = await _stateRepository.GetByIdAsync(id.Value);
            if (State == null)
            {
                return NotFound();
            }

            var model = new CityViewModel { stateId = State.Id };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddCity(CityViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await _stateRepository.AddCityAsync(model);
                return this.RedirectToAction($"Details/{model.stateId}");
            }

            return this.View(model);
        }



        //************************************** STATES *******************************************


        public IActionResult Index()
        {
            return View(_stateRepository.GetStatesWithCities());
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var State = await _stateRepository.GetStateWithCitiesAsync(id.Value);
            if (State == null)
            {
                return NotFound();
            }

            return View(State);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(State state)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _stateRepository.CreateAsync(state);
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There is already a State with that name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }
            }

            return View(state);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var State = await _stateRepository.GetByIdAsync(id.Value);
            if (State == null)
            {
                return NotFound();
            }
            return View(State);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(State state)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _stateRepository.UpdateAsync(state);
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    if (ex.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There is already a State with that name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, ex.InnerException.Message);
                    }
                }             
            }

            return View(state);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var State = await _stateRepository.GetByIdAsync(id.Value);
            if (State == null)
            {
                return NotFound();
            }

            await _stateRepository.DeleteAsync(State);
            return RedirectToAction(nameof(Index));
        }
    }
}
