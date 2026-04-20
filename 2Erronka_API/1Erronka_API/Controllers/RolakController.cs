using Microsoft.AspNetCore.Mvc;
using _1Erronka_API.DTOak;
using _1Erronka_API.Modeloak;
using _1Erronka_API.Repositorioak;

namespace _1Erronka_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolakController : ControllerBase
    {
        private readonly RolaRepository _repo;

        public RolakController(RolaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _repo.GetAll()
                .Select(x => new RolaDto
                {
                    Id = x.Id,
                    Izena = x.Izena
                })
                .ToList();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _repo.Get(id);
            if (item == null) return NotFound();

            return Ok(new RolaDto
            {
                Id = item.Id,
                Izena = item.Izena
            });
        }

        [HttpPost]
        public IActionResult Create([FromBody] RolaDto dto)
        {
            var item = new Rola
            {
                Izena = dto.Izena
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
        public IActionResult Update(int id, [FromBody] RolaDto dto)
        {
            var item = _repo.Get(id);
            if (item == null) return NotFound();

            item.Izena = dto.Izena;

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
