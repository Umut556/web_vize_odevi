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
    public class DerslerController : Controller
    {
        private readonly IDerslerRepository _derslerRepository;
        private readonly IMapper _mapper;

        public DerslerController(IDerslerRepository derslerRepository, IMapper mapper)
        {
            _derslerRepository = derslerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Dersler>))]
        public IActionResult GetDerslers()
        {
            var derslers = _mapper.Map<List<DerslerDto>>(_derslerRepository.GetDerslers());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(derslers);
        }

        [HttpGet("{DersKodu}")]
        [ProducesResponseType(200, Type = typeof(Dersler))]
        [ProducesResponseType(400)]
        public IActionResult GetDersler(string DersKodu)
        {
            if (!_derslerRepository.DerslerExists(DersKodu))
                return NotFound();

            var dersler = _mapper.Map<DerslerDto>(_derslerRepository.GetDersler(DersKodu));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(dersler);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDersler([FromBody] DerslerDto derslerCreate)
        {
            if (derslerCreate == null)
                return BadRequest(ModelState);

            var dersler = _derslerRepository.GetDerslers()
                .Where(c => c.DersKodu.Trim().ToUpper() == derslerCreate.DersKodu.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (dersler != null)
            {
                ModelState.AddModelError("", "Dersler already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var derslerMap = _mapper.Map<Dersler>(derslerCreate);

            if (!_derslerRepository.CreateDersler(derslerMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");


        }
        [HttpPut("{DersKodu}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Updatedersler(string DersKodu, [FromBody] DerslerDto updatedDersler)
        {
            if (updatedDersler == null)
                return BadRequest(ModelState);

            if (DersKodu != updatedDersler.DersKodu)
                return BadRequest(ModelState);

            if (!_derslerRepository.DerslerExists(DersKodu))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var derslerMap = _mapper.Map<Dersler>(updatedDersler);

            if (!_derslerRepository.UpdateDersler(derslerMap))
            {
                ModelState.AddModelError("", "Something went wrong updating dersler");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{DersKodu}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteDersler(string DersKodu)
        {
            if (!_derslerRepository.DerslerExists(DersKodu))
            {
                return NotFound();
            }

            var derslerToDelete = _derslerRepository.GetDersler(DersKodu);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_derslerRepository.DeleteDersler(derslerToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting dersler");
            }

            return NoContent();
        }
    }
}
