using System.Linq;
using System.Threading.Tasks;

namespace AngelsAutomotive.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();


        Task<T> GetByIdAsync(int id);//grabs one


        Task CreateAsync(T entity);


        Task UpdateAsync(T entity);


        Task DeleteAsync(T entity);


        Task<bool> ExistAsync(int id);//checks if exists or not
    }
}
