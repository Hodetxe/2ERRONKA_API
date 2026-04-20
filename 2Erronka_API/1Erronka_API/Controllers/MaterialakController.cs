using Microsoft.AspNetCore.Mvc;
using _1Erronka_API.DTOak;
using _1Erronka_API.Modeloak;
using _1Erronka_API.Repositorioak;

namespace _1Erronka_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialakController : ControllerBase
    {
        private readonly MaterialaRepository _repo;

        public MaterialakController(MaterialaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _repo.GetAll()
                .Select(x => new MaterialaDto
                {
                    Id = x.Id,
                    Izena = x.Izena,
                    Prezioa = x.Prezioa,
                    Stock = x.Stock,
                    HornitzaileakId = x.HornitzaileakId
                })
                .ToList();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _repo.Get(id);
            if (item == null) return NotFound();

            return Ok(new MaterialaDto
            {
                Id = item.Id,
                Izena = item.Izena,
                Prezioa = item.Prezioa,
                Stock = item.Stock,
                HornitzaileakId = item.HornitzaileakId
            });
        }

        [HttpPost]
        public IActionResult Create([FromBody] MaterialaDto dto)
        {
            var item = new Materiala
            {
                Izena = dto.Izena,
                Prezioa = dto.Prezioa,
                Stock = dto.Stock,
                HornitzaileakId = dto.HornitzaileakId
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
        public IActionResult Update(int id, [FromBody] MaterialaDto dto)
        {
            var item = _repo.Get(id);
            if (item == null) return NotFound();

            item.Izena = dto.Izena;
            item.Prezioa = dto.Prezioa;
            item.Stock = dto.Stock;
            item.HornitzaileakId = dto.HornitzaileakId;

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
