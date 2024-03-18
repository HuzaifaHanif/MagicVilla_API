using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO.VillaDTO;
using MagicVilla_VillaAPI.Models.DTO.VillaNumberDTO;

namespace MagicVilla_VillaAPI
{
    public class MappingConfig : Profile
	{
		public MappingConfig() 
		{
			CreateMap<Villa , VillaDTO>().ReverseMap();
			CreateMap<CreateVillaDTO , Villa>().ReverseMap();
			CreateMap<VillaUpdateDTO , Villa>().ReverseMap();

			CreateMap<VillaNumber , VillaNumberDTO>().ReverseMap();
			CreateMap<VillaNumber , VillaNumberCreateDTO>().ReverseMap();
			CreateMap<VillaNumber , VillaNumberUpdateDTO>().ReverseMap();
		}

	}
}
