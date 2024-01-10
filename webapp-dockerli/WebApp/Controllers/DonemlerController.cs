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
    public class DonemlerController : Controller
    {
        private readonly IDonemlerRepository _donemlerRepository;
        private readonly IMapper _mapper;

        public DonemlerController(IDonemlerRepository donemlerRepository, IMapper mapper)
        {
            _donemlerRepository = donemlerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Donemler>))]
        public IActionResult GetDonemlers()
        {
            var donemlers = _mapper.Map<List<DonemlerDto>>(_donemlerRepository.GetDonemlers());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(donemlers);
        }

        [HttpGet("{donemKodu}")]
        [ProducesResponseType(200, Type = typeof(Donemler))]
        [ProducesResponseType(400)]
        public IActionResult GetDonemler(int donemKodu)
        {
            if (!_donemlerRepository.DonemlerExists(donemKodu))
                return NotFound();

            var donemler = _mapper.Map<DonemlerDto>(_donemlerRepository.GetDonemler(donemKodu));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(donemler);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDonemler([FromBody] DonemlerDto donemlerCreate)
        {
            if (donemlerCreate == null)
                return BadRequest(ModelState);

            var donemler = _donemlerRepository.GetDonemlers()
                .Where(c => c.donem.Trim().ToUpper() == donemlerCreate.donem.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (donemler != null)
            {
                ModelState.AddModelError("", "Donemler already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var donemlerMap = _mapper.Map<Donemler>(donemlerCreate);

            if (!_donemlerRepository.CreateDonemler(donemlerMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }

        [HttpPut("{donemKodu}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Updatedonemler(int donemKodu, [FromBody] DonemlerDto updatedDonemler)
        {
            if (updatedDonemler == null)
                return BadRequest(ModelState);

            if (donemKodu != updatedDonemler.donemKod)
                return BadRequest(ModelState);

            if (!_donemlerRepository.DonemlerExists(donemKodu))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var donemlerMap = _mapper.Map<Donemler>(updatedDonemler);

            if (!_donemlerRepository.UpdateDonemler(donemlerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating donemler");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{donemKodu}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteDonemler(int donemKodu)
        {
            if (!_donemlerRepository.DonemlerExists(donemKodu))
            {
                return NotFound();
            }

            var donemlerToDelete = _donemlerRepository.GetDonemler(donemKodu);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_donemlerRepository.DeleteDonemler(donemlerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting donemler");
            }

            return NoContent();
        }
    }
}
