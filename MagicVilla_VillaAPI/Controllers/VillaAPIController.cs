using AutoMapper;
using MagicVilla_VillaAPI.Models;
using MagicVilla_VillaAPI.Models.DTO.VillaDTO;
using MagicVilla_VillaAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace MagicVilla_VillaAPI.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class VillaAPIController : ControllerBase
	{
		private readonly IVillaRepository _dbVilla;
		private readonly IMapper _mapper;
		private readonly APIResponse _response;

		public VillaAPIController(IVillaRepository dbVilla, IMapper mapper)
		{
			_dbVilla = dbVilla;
			_mapper = mapper;
			this._response = new();
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<APIResponse>> GetVillas()
		{
			IEnumerable<Villa> villaList = await _dbVilla.GetAllAsync();

			_response.Result = _mapper.Map<List<VillaDTO>>(villaList);
			_response.StatusCode = HttpStatusCode.OK;
			return Ok(_response);
		}

		[HttpGet("{id:int}" , Name = "GetVilla")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> GetVilla(int id)
		{
			if(id == 0)
			{
				return BadRequest();
			}

			Villa? singleVilla = await _dbVilla.GetAsync(villa => villa.Id == id);

			if(singleVilla == null)
			{
				return NotFound();
			}

			_response.Result = _mapper.Map<VillaDTO>(singleVilla);
			_response.StatusCode = HttpStatusCode.Found;
			_response.IsSuccess	= true;
			return Ok(_response);
		}

		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CreateVilla([FromBody] CreateVillaDTO villaCreate)
		{
			if (villaCreate == null)
			{
				return BadRequest();
			}

			if(await _dbVilla.GetAsync(villa => villa.Name == villaCreate.Name) != null)
			{
				ModelState.AddModelError("CustomerError", "Villa Already exsists");
				return BadRequest(ModelState);

			}
			
			Villa newVilla = _mapper.Map<Villa>(villaCreate);

			await _dbVilla.CreateAsync(newVilla);

			_response.Result = _mapper.Map<VillaDTO>(newVilla);
			_response.StatusCode = HttpStatusCode.Created;
			_response.IsSuccess = true;

			return CreatedAtRoute("GetVilla", new { id = newVilla.Id } , _response);
		}

		[HttpDelete("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> DeleteVilla(int id)
		{
			if(id == 0)
			{
				return BadRequest();
			}

			Villa? villa = await _dbVilla.GetAsync(villa => villa.Id == id);

			if(villa == null)
			{
				return NotFound();
			}

			await _dbVilla.DeleteAsync(villa);
			_response.Result = _mapper.Map<VillaDTO>(villa);
			_response.StatusCode = HttpStatusCode.NoContent;
			_response.IsSuccess = true;
			return Ok(_response);
		}

		[HttpPut("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<APIResponse>> updateVilla(int id, [FromBody] VillaUpdateDTO villaDTO)
		{
			if(villaDTO == null || id != villaDTO.Id)
			{
				return BadRequest();
			}

			Villa? villa = await _dbVilla.GetAsync(villa => villa.Id == id , tracking: false);

			if(villa == null)
			{
				return NotFound();
			}

			Villa updatedVilla = _mapper.Map<Villa>(villaDTO);

			Villa newVilla = await _dbVilla.UpdateVilla(updatedVilla);

			_response.Result = _mapper.Map<VillaDTO>(newVilla);
			_response.StatusCode = HttpStatusCode.NoContent;
			_response.IsSuccess = true;
			return Ok(_response);

		}

	}
}
