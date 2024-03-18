using MagicVilla_VillaAPI.Models;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
		Task CreateAsync(T Entity);

		Task DeleteAsync(T Entity);

		Task<IEnumerable<T>> GetAllAsync();

		Task<T> GetAsync(Expression<Func<T, bool>>? filter, bool tracking = true);

		Task SaveAsync();
	}
}
