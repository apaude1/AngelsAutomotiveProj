using AngelsAutomotive.Data.Entities;
using AngelsAutomotive.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Repositories
{
    public interface IStateRepository : IGenericRepository<State>
    {
		IQueryable GetStatesWithCities();


		Task<State> GetStateWithCitiesAsync(int id);


		Task<City> GetCityAsync(int id);


		Task AddCityAsync(CityViewModel model);


		Task<int> UpdateCityAsync(City city);


		Task<int> DeleteCityAsync(City city);


		IEnumerable<SelectListItem> GetComboStates();


		IEnumerable<SelectListItem> GetComboCities( int stateId);


		Task<State> GetStateAsync(City city);
	}
}
