using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApp.Dto;
using WebApp.Interfaces;
using WebApp.Models;
using WebApp.Repository;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakulteController : Controller
    {
        private readonly IFakulteRepository _fakulteRepository;
        private readonly IMapper _mapper;

        public FakulteController(IFakulteRepository fakulteRepository, IMapper mapper)
        {
            _fakulteRepository = fakulteRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Fakulte>))]
        public IActionResult GetFakultes()
        {
            var fakultes = _mapper.Map<List<FakulteDto>>(_fakulteRepository.GetFakultes());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(fakultes);
        }

        [HttpGet("{FKKod}")]
        [ProducesResponseType(200, Type = typeof(Fakulte))]
        [ProducesResponseType(400)]
        public IActionResult GetFakulte(int FKKod)
        {
            if (!_fakulteRepository.FakulteExists(FKKod))
                return NotFound();

            var fakulte = _mapper.Map<FakulteDto>(_fakulteRepository.GetFakulte(FKKod));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(fakulte);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateFakulte([FromBody] FakulteDto fakulteCreate)
        {
            if (fakulteCreate == null)
                return BadRequest(ModelState);

            var fakulte = _fakulteRepository.GetFakultes()
                .Where(c => c.Adi.Trim().ToUpper() == fakulteCreate.Adi.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (fakulte != null)
            {
                ModelState.AddModelError("", "Fakulte already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var fakulteMap = _mapper.Map<Fakulte>(fakulteCreate);

            if (!_fakulteRepository.CreateFakulte(fakulteMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");


        }

        [HttpPut("{FKKod}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Updatefakulte(int FKKod, [FromBody] FakulteDto updatedFakulte)
        {
            if (updatedFakulte == null)
                return BadRequest(ModelState);

            if (FKKod != updatedFakulte.FKKod)
                return BadRequest(ModelState);

            if (!_fakulteRepository.FakulteExists(FKKod))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var fakulteMap = _mapper.Map<Fakulte>(updatedFakulte);

            if (!_fakulteRepository.UpdateFakulte(fakulteMap))
            {
                ModelState.AddModelError("", "Something went wrong updating fakulte");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{FKKod}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteFakulte(int FKKod)
        {
            if (!_fakulteRepository.FakulteExists(FKKod))
            {
                return NotFound();
            }

            var fakulteToDelete = _fakulteRepository.GetFakulte(FKKod);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_fakulteRepository.DeleteFakulte(fakulteToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting fakulte");
            }

            return NoContent();
        }
    }
}
