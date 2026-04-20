using Microsoft.AspNetCore.Mvc;
using _1Erronka_API.DTOak;
using _1Erronka_API.Modeloak;
using _1Erronka_API.Repositorioak;

namespace _1Erronka_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HornitzaileakController : ControllerBase
    {
        private readonly HornitzaileaRepository _repo;

        public HornitzaileakController(HornitzaileaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _repo.GetAll()
                .Select(x => new HornitzaileaDto
                {
                    Id = x.Id,
                    Izena = x.Izena,
                    Kontaktua = x.Kontaktua,
                    Helbidea = x.Helbidea
                })
                .ToList();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _repo.Get(id);
            if (item == null) return NotFound();

            return Ok(new HornitzaileaDto
            {
                Id = item.Id,
                Izena = item.Izena,
                Kontaktua = item.Kontaktua,
                Helbidea = item.Helbidea
            });
        }

        [HttpPost]
        public IActionResult Create([FromBody] HornitzaileaDto dto)
        {
            var item = new Hornitzailea
            {
                Izena = dto.Izena,
                Kontaktua = dto.Kontaktua,
                Helbidea = dto.Helbidea
            };

            try
            {
                _repo.Add(item);
                dto.Id = item.Id;
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] HornitzaileaDto dto)
        {
            var item = _repo.Get(id);
            if (item == null) return NotFound();

            item.Izena = dto.Izena;
            item.Kontaktua = dto.Kontaktua;
            item.Helbidea = dto.Helbidea;

            try
            {
                _repo.Update(item);
                dto.Id = item.Id;
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _repo.Get(id);
            if (item == null) return NotFound();

            try
            {
                _repo.Delete(item);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
