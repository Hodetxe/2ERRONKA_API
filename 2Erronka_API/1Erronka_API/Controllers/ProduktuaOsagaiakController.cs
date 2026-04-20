using Microsoft.AspNetCore.Mvc;
using _1Erronka_API.DTOak;
using _1Erronka_API.Modeloak;
using _1Erronka_API.Repositorioak;

namespace _1Erronka_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/ProduktuOsagaiak")]
    public class ProduktuaOsagaiakController : ControllerBase
    {
        private readonly ProduktuaOsagaiaRepository _repo;
        private readonly ProduktuaRepository _produktuaRepo;
        private readonly OsagaiaRepository _osagaiaRepo;

        public ProduktuaOsagaiakController(ProduktuaOsagaiaRepository repo, ProduktuaRepository produktuaRepo, OsagaiaRepository osagaiaRepo)
        {
            _repo = repo;
            _produktuaRepo = produktuaRepo;
            _osagaiaRepo = osagaiaRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _repo.GetAll()
                .Select(x => new ProduktuaOsagaiaDto
                {
                    ProduktuaId = x.Produktua.Id,
                    OsagaiaId = x.Osagaia.Id,
                    Kantitatea = x.Kantitatea
                })
                .ToList();

            return Ok(items);
        }

        [HttpGet("{produktuakId:int}/{osagaiakId:int}")]
        public IActionResult Get(int produktuakId, int osagaiakId)
        {
            var item = _repo.Get(produktuakId, osagaiakId);
            if (item == null) return NotFound();

            return Ok(new ProduktuaOsagaiaDto
            {
                ProduktuaId = item.Produktua.Id,
                OsagaiaId = item.Osagaia.Id,
                Kantitatea = item.Kantitatea
            });
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProduktuaOsagaiaDto dto)
        {
            var produktua = _produktuaRepo.Get(dto.ProduktuaId);
            if (produktua == null) return BadRequest("Produktua ez da aurkitu");

            var osagaia = _osagaiaRepo.Get(dto.OsagaiaId);
            if (osagaia == null) return BadRequest("Osagaia ez da aurkitu");

            var item = new ProduktuaOsagaia
            {
                Produktua = produktua,
                Osagaia = osagaia,
                Kantitatea = dto.Kantitatea
            };

            try
            {
                _repo.Add(item);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{produktuakId:int}/{osagaiakId:int}")]
        public IActionResult Update(int produktuakId, int osagaiakId, [FromBody] ProduktuaOsagaiaDto dto)
        {
            var item = _repo.Get(produktuakId, osagaiakId);
            if (item == null) return NotFound();

            item.Kantitatea = dto.Kantitatea;

            try
            {
                _repo.Update(item);
                return Ok(new ProduktuaOsagaiaDto
                {
                    ProduktuaId = item.Produktua.Id,
                    OsagaiaId = item.Osagaia.Id,
                    Kantitatea = item.Kantitatea
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{produktuakId:int}/{osagaiakId:int}")]
        public IActionResult Delete(int produktuakId, int osagaiakId)
        {
            var item = _repo.Get(produktuakId, osagaiakId);
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
