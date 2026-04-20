using Microsoft.AspNetCore.Mvc;
using _1Erronka_API.DTOak;
using _1Erronka_API.Modeloak;
using _1Erronka_API.Repositorioak;

namespace _1Erronka_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ErosketakController : ControllerBase
    {
        private readonly ErosketaRepository _repo;

        public ErosketakController(ErosketaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _repo.GetAll()
                .Select(x => new ErosketaDto
                {
                    Id = x.Id,
                    HornitzaileaId = x.HornitzaileaId,
                    OsagaiaId = x.OsagaiaId,
                    Prezioa = x.Prezioa,
                    Kantitatea = x.Kantitatea,
                    MaterialaId = x.MaterialaId
                })
                .ToList();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _repo.Get(id);
            if (item == null) return NotFound();

            return Ok(new ErosketaDto
            {
                Id = item.Id,
                HornitzaileaId = item.HornitzaileaId,
                OsagaiaId = item.OsagaiaId,
                Prezioa = item.Prezioa,
                Kantitatea = item.Kantitatea,
                MaterialaId = item.MaterialaId
            });
        }

        [HttpPost]
        public IActionResult Create([FromBody] ErosketaDto dto)
        {
            var item = new Erosketa
            {
                HornitzaileaId = dto.HornitzaileaId,
                OsagaiaId = dto.OsagaiaId,
                Prezioa = dto.Prezioa,
                Kantitatea = dto.Kantitatea,
                MaterialaId = dto.MaterialaId
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
        public IActionResult Update(int id, [FromBody] ErosketaDto dto)
        {
            var item = _repo.Get(id);
            if (item == null) return NotFound();

            item.HornitzaileaId = dto.HornitzaileaId;
            item.OsagaiaId = dto.OsagaiaId;
            item.Prezioa = dto.Prezioa;
            item.Kantitatea = dto.Kantitatea;
            item.MaterialaId = dto.MaterialaId;

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
