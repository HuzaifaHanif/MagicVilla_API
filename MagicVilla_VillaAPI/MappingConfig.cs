using AutoMapper;
using MagicVilla_VillaAPI.DTO;
using MagicVilla_VillaAPI.Models;

namespace MagicVilla_VillaAPI
{
	public class MappingConfig : Profile
	{
		public MappingConfig() 
		{
			CreateMap<Villa , VillaDTO>().ReverseMap();
			CreateMap<CreateVillaDTO , Villa>().ReverseMap();
			CreateMap<VillaUpdateDTO , Villa>().ReverseMap();
		}

	}
}
