using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO.LoginDTO;
using Microsoft.AspNetCore.Mvc;

namespace MagicVilla_VillaAPI.Repository.IRepository
{
	public interface IUserRepository
	{
		bool IsUniqueUser(LoginResponseDTO loginDTO);
		
		Task<ActionResult<LoginResponseDTO>> Login(LoginResponseDTO loginDTO);

		Task<LocalUser> RegisterUser(RegisterRequestDTO registerDTO);
	}
}
