using Microsoft.AspNetCore.Mvc;
using _1Erronka_API.Repositorioak;
using _1Erronka_API.DTOak;
using _1Erronka_API.Modeloak;

namespace _1Erronka_API.Controllers
{
    /// <summary>
    /// Produktuak kudeatzeko kontroladorea.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProduktuakController : ControllerBase
    {
        private readonly ProduktuaRepository _repo;

        public ProduktuakController(ProduktuaRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Produktu guztiak lortzen ditu.
        /// </summary>
        /// <returns>Produktu guztien zerrenda DTO formatuan.</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var produktuak = _repo.GetAll();

            var dtoList = produktuak.Select(p => new ProduktuaDto
            {
                Id = p.Id,
                Izena = p.Izena,
                Prezioa = p.Prezioa,
                Mota = p.Mota,
                Stock = p.Stock
            }).ToList();

            return Ok(dtoList);
        }

        /// <summary>
        /// Produktu zehatz bat lortzen du bere IDaren bidez.
        /// </summary>
        /// <param name="id">Produktuaren identifikadorea.</param>
        /// <returns>Produktuaren datuak edo NotFound mezua.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var produktua = _repo.Get(id);
            if (produktua == null) return NotFound();

            var dto = new ProduktuaDto
            {
                Id = produktua.Id,
                Izena = produktua.Izena,
                Prezioa = produktua.Prezioa,
                Mota = produktua.Mota,
                Stock = produktua.Stock
            };

            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProduktuaDto dto)
        {
            var produktua = new Produktua
            {
                Izena = dto.Izena,
                Prezioa = dto.Prezioa,
                Mota = dto.Mota,
                Stock = dto.Stock
            };

            try
            {
                _repo.Add(produktua);
                return Ok(new ProduktuaDto
                {
                    Id = produktua.Id,
                    Izena = produktua.Izena,
                    Prezioa = produktua.Prezioa,
                    Mota = produktua.Mota,
                    Stock = produktua.Stock
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProduktuaDto dto)
        {
            var existing = _repo.Get(id);
            if (existing == null) return NotFound();

            existing.Izena = dto.Izena;
            existing.Prezioa = dto.Prezioa;
            existing.Mota = dto.Mota;
            existing.Stock = dto.Stock;

            try
            {
                _repo.Update(existing);
                return Ok(new ProduktuaDto
                {
                    Id = existing.Id,
                    Izena = existing.Izena,
                    Prezioa = existing.Prezioa,
                    Mota = existing.Mota,
                    Stock = existing.Stock
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
