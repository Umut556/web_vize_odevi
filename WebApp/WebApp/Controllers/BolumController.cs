using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApp.Dto;
using WebApp.Interfaces;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BolumController : Controller
    {
        private readonly IBolumRepository _bolumRepository;
        private readonly IMapper _mapper;
        public BolumController(IBolumRepository bolumRepository, IMapper mapper)
        {
            _bolumRepository = bolumRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200 , Type = typeof(IEnumerable<Bolum>))]
        public IActionResult GetBolums()
        {
            var bolums = _mapper.Map<List<BolumDto>>(_bolumRepository.GetBolums());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bolums);
        }

        [HttpGet("{bolumKod}")]
        [ProducesResponseType(200, Type = typeof(Bolum))]
        [ProducesResponseType(400)]
        public IActionResult GetBolum(int bolumKod) 
        {
            if (!_bolumRepository.BolumExists(bolumKod))
                return NotFound();

            var bolum = _mapper.Map<BolumDto>(_bolumRepository.GetBolum(bolumKod));

            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(bolum);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateBolum([FromBody] BolumDto bolumCreate)
        {
            if(bolumCreate == null)
                return BadRequest(ModelState);

            var bolum = _bolumRepository.GetBolums()
                .Where(c => c.Adi.Trim().ToUpper() == bolumCreate.Adi.TrimEnd().ToUpper())
                .FirstOrDefault();

            if(bolum != null)
            {
                ModelState.AddModelError("", "Bolum already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var bolumMap = _mapper.Map<Bolum>(bolumCreate);

            if (!_bolumRepository.CreateBolum(bolumMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");


        }

        [HttpPut("{bolumKod}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Updatebolum(int bolumKod, [FromBody]BolumDto updatedBolum)
        {
            if(updatedBolum==null) 
                return BadRequest(ModelState);

            if(bolumKod!=updatedBolum.BolumKod)
                return BadRequest(ModelState);

            if(!_bolumRepository.BolumExists(bolumKod))
                return NotFound();

            if(!ModelState.IsValid)
                return BadRequest();

            var bolumMap = _mapper.Map<Bolum>(updatedBolum);

            if(!_bolumRepository.UpdateBolum(bolumMap))
            {
                ModelState.AddModelError("", "Something went wrong updating bolum");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{bolumKod}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteBolum (int bolumKod)
        {
            if (!_bolumRepository.BolumExists(bolumKod))
            {
                return NotFound();
            }

            var bolumToDelete = _bolumRepository.GetBolum(bolumKod);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_bolumRepository.DeleteBolum(bolumToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting bolum");
            }

            return NoContent();
        }



    }
}
