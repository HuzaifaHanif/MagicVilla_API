using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO.VillaNumberDTO;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class VillaNumberController : ControllerBase
	{
		private readonly IVillaNumberRepository _dbVillaNumber;
		private readonly IVillaRepository _dbVilla;
		private readonly IMapper _mapper;
		private readonly APIResponse _response;
		public VillaNumberController(IVillaNumberRepository villaNumber , IMapper mapper , IVillaRepository Villa)
		{
			_dbVillaNumber = villaNumber;
			_dbVilla = Villa;
			_mapper = mapper;
			this._response = new();
		}


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetALL()
		{
			IEnumerable<VillaNumber> villaNumberList = await _dbVillaNumber.GetAllAsync();

			_response.Result =  _mapper.Map<List<VillaNumberDTO>>(villaNumberList);
			_response.IsSuccess = true;
			_response.StatusCode = HttpStatusCode.OK;
			return Ok(_response);
		}

		[HttpGet("{villaNumber:int}",Name = "GetVillaNumber")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<ActionResult<APIResponse>> GetVillaNumber(int villaNumber)
		{
			if(villaNumber == 0)
			{
				return BadRequest();
			}

			VillaNumber? villa = await _dbVillaNumber.GetAsync(item => item.VillaNo == villaNumber);

			if(villa == null)
			{
				return NotFound();
			}

			_response.Result = _mapper.Map<VillaNumberDTO>(villa);
			_response.IsSuccess = true;
			_response.StatusCode = HttpStatusCode.Found;

			return Ok(_response);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateVillaNumber([FromBody] VillaNumberCreateDTO createDTO)
		{
			if(createDTO == null )
			{
				return BadRequest();
			}

			if(await _dbVillaNumber.GetAsync(item => item.VillaNo == createDTO.VillaNo) != null)
			{
				ModelState.AddModelError("CustomerError", "Villa number Already exsists");
				return BadRequest(ModelState);
			}

			if(await _dbVilla.GetAsync(item => item.Id == createDTO.VillaId) == null)
			{
				ModelState.AddModelError("CustomerError", "Villa Id Already exsists");
				return BadRequest(ModelState);

			}

			VillaNumber createdVillaNumber = _mapper.Map<VillaNumber>(createDTO);

			await _dbVillaNumber.CreateAsync(createdVillaNumber);

			_response.Result = createdVillaNumber;
			_response.StatusCode = HttpStatusCode.Created;
			_response.IsSuccess = true;

			return Ok(_response);
		}

		[HttpDelete("{villaNumber:int}", Name = "DeleteVilla")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<ActionResult<APIResponse>> DeleteVillaNumber(int villaNumber)
		{
			if (villaNumber == 0)
			{
				return BadRequest();
			}

			VillaNumber deleteVilla = await _dbVillaNumber.GetAsync(item => item.VillaNo == villaNumber);

			if (deleteVilla == null)
			{
				return NotFound();

			}

			await _dbVillaNumber.DeleteAsync(deleteVilla);

			_response.Result = deleteVilla;
			_response.StatusCode = HttpStatusCode.Found;
			_response.IsSuccess = true;
			return Ok(_response);
		}

		[HttpPut("{villaNumber:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]

		public async Task<ActionResult<APIResponse>> UpdateVillaNumber(int villaNumber , [FromBody] VillaNumberUpdateDTO updateDTO)
		{
			if(villaNumber == 0 || updateDTO == null || villaNumber != updateDTO.VillaNo)
			{
				return BadRequest();
			}

			if (await _dbVilla.GetAsync(item => item.Id == updateDTO.VillaId) == null)
			{
				ModelState.AddModelError("CustomerError", "Villa Id Already exsists");
				return BadRequest(ModelState);

			}

			VillaNumber? updateVilla = await _dbVillaNumber.GetAsync(item => item.VillaNo == villaNumber, false);

			if(updateVilla == null)
			{
				_response.StatusCode = HttpStatusCode.NotFound;
				_response.IsSuccess = false;
				return NotFound(_response);
			}

			VillaNumber updatedVilla = _mapper.Map<VillaNumber>(updateDTO);

			await _dbVillaNumber.UpdateVillaNumberAsync(updatedVilla);
			_response.Result = updatedVilla;
			_response.StatusCode = HttpStatusCode.NoContent; 
			_response.IsSuccess = true;
			return Ok(_response);

		}








	}
}
