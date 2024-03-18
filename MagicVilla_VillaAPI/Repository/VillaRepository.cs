using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVilla_VillaAPI.Repository
{
    public class VillaRepository : Repository<Villa> , IVillaRepository
	{
		private readonly ApplicationDbContext _db;
		public VillaRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public async Task<Villa> UpdateVilla(Villa Entity)
		{
			_db.Villas.Update(Entity);
			await _db.SaveChangesAsync();
			return Entity;
		}
			

	}
}
