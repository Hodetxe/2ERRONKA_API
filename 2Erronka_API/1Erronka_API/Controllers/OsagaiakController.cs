using Microsoft.AspNetCore.Mvc;
using _1Erronka_API.Repositorioak;
using _1Erronka_API.DTOak;
using _1Erronka_API.Modeloak;

namespace _1Erronka_API.Controllers
{
    /// <summary>
    /// Osagaiak kudeatzeko kontroladorea.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OsagaiakController : ControllerBase
    {
        private readonly OsagaiaRepository _repo;

        public OsagaiakController(OsagaiaRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Osagai guztiak lortzen ditu.
        /// </summary>
        /// <returns>Osagai guztien zerrenda DTO formatuan.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var osagaiak = _repo.GetAll();

            var dtoList = osagaiak.Select(o => new OsagaiaDto
            {
                Id = o.Id,
                Izena = o.Izena,
                Prezioa = o.Prezioa,
                Stock = o.Stock,
                HornitzaileakId = o.HornitzaileakId
            }).ToList();

            return Ok(dtoList);
        }

        /// <summary>
        /// Osagai zehatz bat lortzen du bere IDaren bidez.
        /// </summary>
        /// <param name="id">Osagaiaren identifikadorea.</param>
        /// <returns>Osagaiaren datuak edo NotFound mezua.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var osagaia = _repo.Get(id);
            if (osagaia == null) return NotFound();

            var dto = new OsagaiaDto
            {
                Id = osagaia.Id,
                Izena = osagaia.Izena,
                Prezioa = osagaia.Prezioa,
                Stock = osagaia.Stock,
                HornitzaileakId = osagaia.HornitzaileakId
            };

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] OsagaiaDto dto)
        {
            var osagaia = new Osagaia
            {
                Izena = dto.Izena,
                Prezioa = dto.Prezioa,
                Stock = dto.Stock,
                HornitzaileakId = dto.HornitzaileakId
            };

            try
            {
                _repo.Add(osagaia);
                return Ok(new OsagaiaDto
                {
                    Id = osagaia.Id,
                    Izena = osagaia.Izena,
                    Prezioa = osagaia.Prezioa,
                    Stock = osagaia.Stock,
                    HornitzaileakId = osagaia.HornitzaileakId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] OsagaiaDto dto)
        {
            var existing = _repo.Get(id);
            if (existing == null) return NotFound();

            existing.Izena = dto.Izena;
            existing.Prezioa = dto.Prezioa;
            existing.Stock = dto.Stock;
            existing.HornitzaileakId = dto.HornitzaileakId;

            try
            {
                _repo.Update(existing);
                return Ok(new OsagaiaDto
                {
                    Id = existing.Id,
                    Izena = existing.Izena,
                    Prezioa = existing.Prezioa,
                    Stock = existing.Stock,
                    HornitzaileakId = existing.HornitzaileakId
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _repo.Get(id);
            if (existing == null) return NotFound();

            try
            {
                _repo.Delete(existing);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
