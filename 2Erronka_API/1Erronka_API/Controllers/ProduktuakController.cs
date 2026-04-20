using Microsoft.AspNetCore.Mvc;
using _1Erronka_API.Repositorioak;
using _1Erronka_API.DTOak;
using _1Erronka_API.Modeloak;

namespace _1Erronka_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProduktuakController : ControllerBase
    {
        private readonly ProduktuaRepository _repo;
        private readonly ProduktuaOsagaiaRepository _poRepo;
        private readonly OsagaiaRepository _osagaiaRepo;

        public ProduktuakController(ProduktuaRepository repo, ProduktuaOsagaiaRepository poRepo, OsagaiaRepository osagaiaRepo)
        {
            _repo = repo;
            _poRepo = poRepo;
            _osagaiaRepo = osagaiaRepo;
        }

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

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var produktua = _repo.Get(id);
            if (produktua == null) return NotFound();
            return Ok(new ProduktuaDto
            {
                Id = produktua.Id,
                Izena = produktua.Izena,
                Prezioa = produktua.Prezioa,
                Mota = produktua.Mota,
                Stock = produktua.Stock
            });
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
            _repo.Add(produktua);
            return Ok(new ProduktuaDto { Id = produktua.Id, Izena = produktua.Izena, Prezioa = produktua.Prezioa, Mota = produktua.Mota, Stock = produktua.Stock });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProduktuaDto dto)
        {
            var produktua = _repo.Get(id);
            if (produktua == null) return NotFound();
            produktua.Izena = dto.Izena;
            produktua.Prezioa = dto.Prezioa;
            produktua.Mota = dto.Mota;
            produktua.Stock = dto.Stock;
            _repo.Update(produktua);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var produktua = _repo.Get(id);
            if (produktua == null) return NotFound();
            _repo.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}/stock")]
        public IActionResult UpdateStock(int id, [FromBody] Dictionary<string, int> body)
        {
            if (!body.TryGetValue("stock", out int stock)) return BadRequest();
            var produktua = _repo.Get(id);
            if (produktua == null) return NotFound();
            _repo.UpdateStock(id, stock);
            return NoContent();
        }

        [HttpGet("{id}/osagaiak")]
        public IActionResult GetOsagaiak(int id)
        {
            var produktua = _repo.Get(id);
            if (produktua == null) return NotFound();
            var erlazio = _poRepo.GetByProduktuaId(id);
            var result = erlazio.Select(po => new ProduktuOsagaiaErantzunDto
            {
                ProduktuaId = id,
                OsagaiaId = po.Osagaia.Id,
                Izena = po.Osagaia.Izena,
                Kantitatea = po.Kantitatea,
                Unitatea = "unit"
            }).ToList();
            return Ok(result);
        }

        [HttpPost("{id}/osagaiak")]
        public IActionResult AddOsagaia(int id, [FromBody] ProduktuOsagaiaBerriaDto dto)
        {
            var produktua = _repo.Get(id);
            if (produktua == null) return NotFound();
            var osagaia = _osagaiaRepo.Get(dto.OsagaiaId);
            if (osagaia == null) return NotFound();
            var existing = _poRepo.Get(id, dto.OsagaiaId);
            if (existing != null)
            {
                existing.Kantitatea = (int)Math.Round(dto.Kantitatea);
                _poRepo.Update(existing);
            }
            else
            {
                var po = new ProduktuaOsagaia
                {
                    Produktua = produktua,
                    Osagaia = osagaia,
                    Kantitatea = (int)Math.Round(dto.Kantitatea)
                };
                _poRepo.Add(po);
            }
            return Ok();
        }

        [HttpDelete("{id}/osagaiak/{osagaiaId}")]
        public IActionResult RemoveOsagaia(int id, int osagaiaId)
        {
            var po = _poRepo.Get(id, osagaiaId);
            if (po == null) return NotFound();
            _poRepo.Delete(po);
            return NoContent();
        }

        [HttpPut("{id}/osagaiak/{osagaiaId}")]
        public IActionResult UpdateOsagaia(int id, int osagaiaId, [FromBody] ProduktuOsagaiaBerriaDto dto)
        {
            var po = _poRepo.Get(id, osagaiaId);
            if (po == null) return NotFound();
            po.Kantitatea = (int)Math.Round(dto.Kantitatea);
            _poRepo.Update(po);
            return NoContent();
        }
    }
}
