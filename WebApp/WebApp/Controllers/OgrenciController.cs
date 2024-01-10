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
    public class OgrenciController : Controller
    {
        private readonly IOgrenciRepository _ogrenciRepository;
        private readonly IMapper _mapper;

        public OgrenciController(IOgrenciRepository ogrenciRepository, IMapper mapper)
        {
            _ogrenciRepository = ogrenciRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ogrenci>))]
        public IActionResult GetOgrencis()
        {
            var ogrencis = _mapper.Map<List<OgrenciDto>>(_ogrenciRepository.GetOgrencis());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ogrencis);
        }

        [HttpGet("{OgrNo}")]
        [ProducesResponseType(200, Type = typeof(Ogrenci))]
        [ProducesResponseType(400)]
        public IActionResult GetOgrenci(string OgrNo)
        {
            if (!_ogrenciRepository.OgrenciExists(OgrNo))
                return NotFound();

            var ogrenci = _mapper.Map<OgrenciDto>(_ogrenciRepository.GetOgrenci(OgrNo));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ogrenci);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateOgrenci([FromBody] OgrenciDto ogrenciCreate)
        {
            if (ogrenciCreate == null)
                return BadRequest(ModelState);

            var ogrenci = _ogrenciRepository.GetOgrencis()
                .Where(c => c.Adi.Trim().ToUpper() == ogrenciCreate.Adi.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (ogrenci != null)
            {
                ModelState.AddModelError("", "Ogrenci already exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ogrenciMap = _mapper.Map<Ogrenci>(ogrenciCreate);

            if (!_ogrenciRepository.CreateOgrenci(ogrenciMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");


        }

        [HttpPut("{OgrNo}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult Updateogrenci(string OgrNo, [FromBody] OgrenciDto updatedOgrenci)
        {
            if (updatedOgrenci == null)
                return BadRequest(ModelState);

            if (OgrNo != updatedOgrenci.OgrNo)
                return BadRequest(ModelState);

            if (!_ogrenciRepository.OgrenciExists(OgrNo))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var ogrenciMap = _mapper.Map<Ogrenci>(updatedOgrenci);

            if (!_ogrenciRepository.UpdateOgrenci(ogrenciMap))
            {
                ModelState.AddModelError("", "Something went wrong updating ogrenci");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{OgrNo}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteOgrenci(string OgrNo)
        {
            if (!_ogrenciRepository.OgrenciExists(OgrNo))
            {
                return NotFound();
            }

            var ogrenciToDelete = _ogrenciRepository.GetOgrenci(OgrNo);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_ogrenciRepository.DeleteOgrenci(ogrenciToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting ogrenci");
            }

            return NoContent();
        }
    }
}
