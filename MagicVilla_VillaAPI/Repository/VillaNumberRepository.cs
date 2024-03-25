using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
	{
		private readonly ApplicationDbContext _db;
		public VillaNumberRepository(ApplicationDbContext db) :base(db)
		{
			_db = db;
		}

		public async Task<VillaNumber> UpdateVillaNumberAsync(VillaNumber Entity)
		{
			_db.VillaNumbers.Update(Entity);
			await _db.SaveChangesAsync();
			return Entity;
			
		}
	}
}
