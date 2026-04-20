using Microsoft.AspNetCore.Mvc;
using _1Erronka_API.DTOak;
using _1Erronka_API.Modeloak;
using _1Erronka_API.Repositorioak;

namespace _1Erronka_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/EskariakHasProduktuak")]
    public class EskariaProduktuakController : ControllerBase
    {
        private readonly EskariaProduktuaRepository _repo;
        private readonly EskariaRepository _eskariaRepo;
        private readonly ProduktuaRepository _produktuaRepo;

        public EskariaProduktuakController(EskariaProduktuaRepository repo, EskariaRepository eskariaRepo, ProduktuaRepository produktuaRepo)
        {
            _repo = repo;
            _eskariaRepo = eskariaRepo;
            _produktuaRepo = produktuaRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _repo.GetAll()
                .Select(x => new EskariaProduktuaLinkDto
                {
                    EskariaId = x.Eskaria.Id,
                    ProduktuaId = x.Produktua.Id,
                    Kantitatea = x.Kantitatea,
                    Prezioa = x.Prezioa
                })
                .ToList();

            return Ok(items);
        }

        [HttpGet("{eskariakId:int}/{produktuakId:int}")]
        public IActionResult Get(int eskariakId, int produktuakId)
        {
            var item = _repo.Get(eskariakId, produktuakId);
            if (item == null) return NotFound();

            return Ok(new EskariaProduktuaLinkDto
            {
                EskariaId = item.Eskaria.Id,
                ProduktuaId = item.Produktua.Id,
                Kantitatea = item.Kantitatea,
                Prezioa = item.Prezioa
            });
        }

        [HttpPost]
        public IActionResult Create([FromBody] EskariaProduktuaLinkDto dto)
        {
            var eskaria = _eskariaRepo.Get(dto.EskariaId);
            if (eskaria == null) return BadRequest("Eskaria ez da aurkitu");

            var produktua = _produktuaRepo.Get(dto.ProduktuaId);
            if (produktua == null) return BadRequest("Produktua ez da aurkitu");

            var item = new EskariaProduktua
            {
                Eskaria = eskaria,
                Produktua = produktua,
                Kantitatea = dto.Kantitatea,
                Prezioa = dto.Prezioa
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

        [HttpPut("{eskariakId:int}/{produktuakId:int}")]
        public IActionResult Update(int eskariakId, int produktuakId, [FromBody] EskariaProduktuaLinkDto dto)
        {
            var item = _repo.Get(eskariakId, produktuakId);
            if (item == null) return NotFound();

            item.Kantitatea = dto.Kantitatea;
            item.Prezioa = dto.Prezioa;

            try
            {
                _repo.Update(item);
                return Ok(new EskariaProduktuaLinkDto
                {
                    EskariaId = item.Eskaria.Id,
                    ProduktuaId = item.Produktua.Id,
                    Kantitatea = item.Kantitatea,
                    Prezioa = item.Prezioa
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{eskariakId:int}/{produktuakId:int}")]
        public IActionResult Delete(int eskariakId, int produktuakId)
        {
            var item = _repo.Get(eskariakId, produktuakId);
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
