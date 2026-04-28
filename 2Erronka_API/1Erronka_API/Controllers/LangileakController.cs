using Microsoft.AspNetCore.Mvc;
using _1Erronka_API.Domain;
using _1Erronka_API.DTOak;
using _1Erronka_API.Repositorioak;

namespace _1Erronka_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Route("api/Erabiltzaileak")]
    public class LangileakController : ControllerBase
    {
        private readonly LangileaRepository _repo;

        public LangileakController(LangileaRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var items = _repo.GetAll()
                .Select(x => new LangileaCrudDto
                {
                    Id = x.Id,
                    Izena = x.Izena,
                    Abizena = x.Abizena,
                    Erabiltzaile_izena = x.Erabiltzaile_izena,
                    Langile_kodea = x.Langile_kodea,
                    Pasahitza = x.Pasahitza,
                    RolaId = x.RolaId,
                    Ezabatua = x.Ezabatua,
                    Chat = x.Chat
                })
                .ToList();

            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _repo.Get(id);
            if (item == null) return NotFound();

            return Ok(new LangileaCrudDto
            {
                Id = item.Id,
                Izena = item.Izena,
                Abizena = item.Abizena,
                Erabiltzaile_izena = item.Erabiltzaile_izena,
                Langile_kodea = item.Langile_kodea,
                Pasahitza = item.Pasahitza,
                RolaId = item.RolaId,
                Ezabatua = item.Ezabatua,
                Chat = item.Chat
            });
        }

        [HttpPost]
        public IActionResult Create([FromBody] LangileaCrudDto dto)
        {
            var item = new Langilea
            {
                Izena = dto.Izena,
                Abizena = dto.Abizena,
                Erabiltzaile_izena = dto.Erabiltzaile_izena,
                Langile_kodea = dto.Langile_kodea,
                Pasahitza = dto.Pasahitza,
                RolaId = dto.RolaId,
                Ezabatua = dto.Ezabatua,
                Chat = dto.Chat
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
        public IActionResult Update(int id, [FromBody] LangileaCrudDto dto)
        {
            var item = _repo.Get(id);
            if (item == null) return NotFound();

            item.Izena = dto.Izena;
            item.Abizena = dto.Abizena;
            item.Erabiltzaile_izena = dto.Erabiltzaile_izena;
            item.Langile_kodea = dto.Langile_kodea;
            item.Pasahitza = dto.Pasahitza;
            item.RolaId = dto.RolaId;
            item.Ezabatua = dto.Ezabatua;
            item.Chat = dto.Chat;

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
