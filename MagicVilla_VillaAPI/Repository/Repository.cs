using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using MagicVilla_VillaAPI.Repository.IRepository;
using MagicVilla_VillaAPI.Models.DTO;

namespace MagicVilla_VillaAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		private DbSet<T> dbSet;
		public Repository(ApplicationDbContext db)
		{
			_db = db;
			this.dbSet = _db.Set<T>();
		}

		public async Task CreateAsync(T Entity)
		{
			await dbSet.AddAsync(Entity);
			await SaveAsync();
		}

		public async Task DeleteAsync(T Entity)
		{
			dbSet.Remove(Entity);
			await SaveAsync();
		}

		public async Task<IEnumerable<T>> GetAllAsync()
		{
			IQueryable<T> query = dbSet;
			return await query.ToListAsync();
		}

		public async Task<T> GetAsync(Expression<Func<T, bool>>? filter = null, bool tracking = true)
		{
			IQueryable<T> query = dbSet;

			if (filter != null)
			{
				query = query.Where(filter);
			}

			if (!tracking)
			{
				query = query.AsNoTracking();
			}

			return await query.FirstOrDefaultAsync();
		}

		public async Task SaveAsync()
		{
			await _db.SaveChangesAsync();
		}
	}
}
