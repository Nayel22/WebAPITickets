using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPITickets.Database;
using WebAPITickets.Models;

namespace WebAPITickets.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly ContextoBD _contexto;

        public UsuariosController(ContextoBD contexto)
        {
            _contexto = contexto;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        {
            return await _contexto.Usuarios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetUsuarioById(int id)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPost]
        [Route("crear")]
        public async Task<ActionResult<Usuarios>> PostUsuario([FromBody] Usuarios usuario)
        {
            _contexto.Usuarios.Add(usuario);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.us_identificador }, usuario);
        }

        [HttpPut]
        [Route("actualizar/{id}")]
        public async Task<IActionResult> PutUsuario(int id, [FromBody] Usuarios usuario)
        {
            if (id != usuario.us_identificador)
            {
                return BadRequest();
            }

            _contexto.Entry(usuario).State = EntityState.Modified;

            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_contexto.Usuarios.Any(e => e.us_identificador == id))
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
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _contexto.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _contexto.Usuarios.Remove(usuario);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}