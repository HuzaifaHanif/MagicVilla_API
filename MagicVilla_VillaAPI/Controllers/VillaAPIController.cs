using AutoMapper;
using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.DTO;
using MagicVilla_VillaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VillaAPIController : ControllerBase
	{
		private readonly ApplicationDbContext _db;
		private readonly IMapper _mapper;

		public VillaAPIController(ApplicationDbContext context , IMapper mapper)
		{
			_db = context;
			_mapper = mapper;
		}


		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<VillaDTO>>> GetVillas()
		{
			IEnumerable<Villa> villaList = await _db.Villas.ToListAsync();

			return Ok(_mapper.Map<List<VillaDTO>>(villaList));
		}

		[HttpGet("{id:int}" , Name = "GetVilla")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<VillaDTO>> GetVilla(int id)
		{
			if(id == 0)
			{
				return BadRequest();
			}

			Villa? singleVilla = await _db.Villas.FirstOrDefaultAsync(villa => villa.Id == id);

			if(singleVilla == null)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<VillaDTO>(singleVilla));
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

			if(await _db.Villas.FirstOrDefaultAsync(villa => villa.Name == villaCreate.Name) != null)
			{
				ModelState.AddModelError("CustomerError", "Villa Already exsists");
				return BadRequest(ModelState);

			}
			
			Villa newVilla = _mapper.Map<Villa>(villaCreate);

			await _db.Villas.AddAsync(newVilla);
			await _db.SaveChangesAsync();
			return CreatedAtRoute("GetVilla", new { id = newVilla.Id } , newVilla);
		}

		[HttpDelete("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteVilla(int id)
		{
			if(id == 0)
			{
				return BadRequest();
			}

			Villa? villa = await _db.Villas.SingleOrDefaultAsync(villa => villa.Id == id);

			if(villa == null)
			{
				return NotFound();
			}

			_db.Villas.Remove(villa);
			await _db.SaveChangesAsync();
			return NoContent();
		}

		[HttpPut("{id:int}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult> updateVilla(int id, [FromBody] VillaUpdateDTO villaDTO)
		{
			if(villaDTO == null || id != villaDTO.Id)
			{
				return BadRequest();
			}

			Villa? villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(villa => villa.Id == id);

			if(villa == null)
			{
				return NotFound();
			}

			Villa updatedVilla = _mapper.Map<Villa>(villaDTO);

			_db.Villas.Update(updatedVilla);
			await _db.SaveChangesAsync();
			return NoContent();

		}

	}
}
