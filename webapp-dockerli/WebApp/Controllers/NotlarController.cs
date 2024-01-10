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
    public class NotlarController : Controller
    {
        private readonly INotlarRepository _notlarRepository;
        private readonly IMapper _mapper;

        public NotlarController(INotlarRepository notlarRepository, IMapper mapper)
        {
            _notlarRepository = notlarRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Notlar>))]
        public IActionResult GetNotlars()
        {
            var notlars = _mapper.Map<List<NotlarDto>>(_notlarRepository.GetNotlars());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(notlars);
        }

        [HttpGet("{ID}")]
        [ProducesResponseType(200, Type = typeof(Notlar))]
        [ProducesResponseType(400)]
        public IActionResult GetNotlar(int ID)
        {
            if (!_notlarRepository.NotlarExists(ID))
                return NotFound();

            var notlar = _mapper.Map<NotlarDto>(_notlarRepository.GetNotlar(ID));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(notlar);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateNotlar([FromBody] NotlarDto notlarCreate)
        {
            if (notlarCreate == null)
                return BadRequest(ModelState);

            var notlar = _notlarRepository.GetNotlars()
                .Where(c => c.ogrno.Trim().ToUpper() == notlarCreate.ogrno.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (notlar != null)
            {
                ModelState.AddModelError("", "Notlar already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var notlarMap = _mapper.Map<Notlar>(notlarCreate);

            if (!_notlarRepository.CreateNotlar(notlarMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");


        }

        [HttpPut("{ID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Updatenotlar(int ID, [FromBody] NotlarDto updatedNotlar)
        {
            if (updatedNotlar == null)
                return BadRequest(ModelState);

            if (ID != updatedNotlar.ID)
                return BadRequest(ModelState);

            if (!_notlarRepository.NotlarExists(ID))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var notlarMap = _mapper.Map<Notlar>(updatedNotlar);

            if (!_notlarRepository.UpdateNotlar(notlarMap))
            {
                ModelState.AddModelError("", "Something went wrong updating notlar");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{ID}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteNotlar(int ID)
        {
            if (!_notlarRepository.NotlarExists(ID))
            {
                return NotFound();
            }

            var notlarToDelete = _notlarRepository.GetNotlar(ID);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_notlarRepository.DeleteNotlar(notlarToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting notlar");
            }

            return NoContent();
        }
    }
}
