using AngelsAutomotive.Data.Entities;
using AngelsAutomotive.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Repositories
{
    public class StateRepository : GenericRepository<State>, IStateRepository
    {
        private readonly DataContext _context;

        public StateRepository(DataContext context) : base(context)
        {
            _context = context;
        }



        public async Task AddCityAsync(CityViewModel model)
        {
            var State = await this.GetStateWithCitiesAsync(model.stateId);
            if (State == null)
            {
                return;
            }

            State.Cities.Add(new City { Name = model.Name });
            _context.States.Update(State);
            await _context.SaveChangesAsync();

        }


        public async Task<int> DeleteCityAsync(City city)
        {
            var State = await _context.States.Where(c => c.Cities.Any(ci => ci.Id == city.Id)).FirstOrDefaultAsync();
            if (State == null)
            {
                return 0;
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return State.Id;
        }


        public async Task<City> GetCityAsync(int id)
        {
            return await _context.Cities.FindAsync(id);
        }



        public IEnumerable<SelectListItem> GetComboCities(int stateId)
        {
            //var State = _context.States.Find(stateId);
            //var list = new List<SelectListItem>();
            //if (State != null)
            //{
            //    list = State.Cities.Select(c => new SelectListItem
            //    {
            //        Text = c.Name,
            //        Value = c.Id.ToString()
            //    }).OrderBy(l => l.Text).ToList();
            //}

            var list = _context.Cities.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()

            }).OrderBy(l => l.Text).ToList();

            list.Insert(0, new SelectListItem
            { 
                Text = "(Select a city...)",
                Value = "0"
            });

            return list;

        }


        public IEnumerable<SelectListItem> GetComboStates()
        {
            var list = _context.States.Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()

            }).OrderBy(l => l.Text).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a State...)",
                Value = "0"
            });

            return list;

        }

        public IQueryable GetStatesWithCities()
        {
            return _context.States
            .Include(c => c.Cities)
            .OrderBy(c => c.Name);

        }


        public async Task<State> GetStateAsync(City city)
        {
            return await _context.States.Where(c => c.Cities.Any(ci => ci.Id == city.Id)).FirstOrDefaultAsync();
        }


        public async Task<State> GetStateWithCitiesAsync(int id)
        {
            return await _context.States
             .Include(c => c.Cities)
             .Where(c => c.Id == id)
             .FirstOrDefaultAsync();

        }


        public async Task<int> UpdateCityAsync(City city)
        {
            var State = await _context.States.Where(c => c.Cities.Any(ci => ci.Id == city.Id)).FirstOrDefaultAsync();
            if (State == null)
            {
                return 0;
            }

            _context.Cities.Update(city);
            await _context.SaveChangesAsync();
            return State.Id;
        }

    }
}
