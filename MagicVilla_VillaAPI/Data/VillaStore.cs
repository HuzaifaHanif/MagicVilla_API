using MagicVilla_VillaAPI.DTO;

namespace MagicVilla_VillaAPI.Data
{
	public static class VillaStore
	{
		public static List<VillaDTO> villaList = new List<VillaDTO>
		{
			new VillaDTO
			{
				Id = 1, Name ="Huzaifa" , Details = "hello my name is huzaifa" , 
				Rate = 123.5 , Sqft = 200 
			},
			new VillaDTO
			{
				Id = 2, Name ="Zeeshan" , Details = "hello my name is Zeeshan" ,
				Rate = 200 , Sqft = 400
			}
		};

	}
}
