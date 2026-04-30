using Microsoft.AspNetCore.Mvc;
using _1Erronka_API.DTOak;
using _1Erronka_API.Modeloak;
using _1Erronka_API.Repositorioak;

namespace _1Erronka_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeskontuakController : ControllerBase
    {
        private readonly DeskontuaRepository _repo;

        public DeskontuakController(DeskontuaRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("upsert")]
        public IActionResult Upsert([FromBody] DeskontuaUpsertDto dto)
        {
            try
            {
                var kodea = (dto.Kodea ?? string.Empty).Trim();
                if (kodea.Length == 0) return BadRequest("Kodea beharrezkoa da");

                var mota = (dto.Mota ?? string.Empty).Trim();
                if (mota.Length == 0) return BadRequest("Mota beharrezkoa da");

                var existing = _repo.GetByKodea(kodea);
                if (existing == null)
                {
                    existing = new Deskontua
                    {
                        Kodea = kodea,
                        Mota = mota,
                        Balioa = dto.Balioa,
                        Aktibo = dto.Aktibo
                    };
                }
                else
                {
                    existing.Mota = mota;
                    existing.Balioa = dto.Balioa;
                    existing.Aktibo = dto.Aktibo;
                }

                _repo.SaveOrUpdate(existing);
                return Ok(new { Ok = true, Id = existing.Id, Kodea = existing.Kodea });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
