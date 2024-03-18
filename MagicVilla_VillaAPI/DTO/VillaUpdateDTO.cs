using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.DTO
{
	public class VillaUpdateDTO
	{
		[Required]
		public int Id { get; set; }

		public string Name { get; set; }

		public string Details { get; set; }

		public double Rate { get; set; }

		public int Sqft { get; set; }
	}
}
