using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPITickets.Database;
using WebAPITickets.Models;

namespace WebAPITickets.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiquetesController : Controller
    {
        private readonly ContextoBD _contexto;

        public TiquetesController(ContextoBD contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tiquetes>>> GetTiquetes()
        {
            return await _contexto.Tiquetes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tiquetes>> GetTiqueteById(int id)
        {
            var tiquete = await _contexto.Tiquetes.FindAsync(id);

            if (tiquete == null)
            {
                return NotFound();
            }

            return tiquete;
        }

        [HttpPost]
        [Route("crear")]
        public async Task<ActionResult<Tiquetes>> PostTiquete([FromBody] Tiquetes tiquete)
        {
            _contexto.Tiquetes.Add(tiquete);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTiqueteById), new { id = tiquete.ti_identificador }, tiquete);
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public async Task<IActionResult> PutTiquete(int id, [FromBody] Tiquetes tiquete)
        {
            if (id != tiquete.ti_identificador)
            {
                return BadRequest();
            }

            _contexto.Entry(tiquete).State = EntityState.Modified;

            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_contexto.Tiquetes.Any(e => e.ti_identificador == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public async Task<IActionResult> DeleteTiquete(int id)
        {
            var tiquete = await _contexto.Tiquetes.FindAsync(id);
            if (tiquete == null)
            {
                return NotFound();
            }

            _contexto.Tiquetes.Remove(tiquete);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}