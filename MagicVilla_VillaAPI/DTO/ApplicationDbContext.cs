using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.DTO
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) 
		{ }

		public DbSet<Villa> Villas { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Villa>().HasData(

				new Villa()
				{
					Id = 1,
					Name = "Huzaifa",
					Details = "hello my name is huzaifa",
					Rate = 123.5,
					Sqft = 200
				},
				new Villa()
				{
					Id = 2,
					Name = "Zeeshan",
					Details = "hello my name is Zeeshan",
					Rate = 200,
					Sqft = 400
				},
				new Villa()
				{
					Id = 3,
					Name = "Moosa",
					Details = "hello my name is moosa",
					Rate = 123.5,
					Sqft = 200
				},
				new Villa()
				{
					Id = 4,
					Name = "Faisal",
					Details = "hello my name is Faisal",
					Rate = 200,
					Sqft = 400
				}
			);

		}


	}
}
