using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPITickets.Database;
using WebAPITickets.Models;

namespace WebAPITickets.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly ContextoBD _contexto;

        public RolesController(ContextoBD contexto)
        {

            _contexto = contexto;

        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Roles>>> GetRol()

        {

            return await _contexto.Roles.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Roles>> GetRolById(int id)
        {
            var rol = await _contexto.Roles.FindAsync(id);

            if (rol == null)
            {
                return NotFound();
            }

            return rol;
        }

        [HttpPost]
        [Route("crear")]
        public async Task<ActionResult<Roles>> PostRol([FromBody] Roles rol)
        {
            _contexto.Roles.Add(rol);
            await _contexto.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRolById), new { id = rol.ro_identificador }, rol);
        }


        [HttpPut]
        [Route("actualizar/{id}")]
        public async Task<IActionResult> PutRol(int id, [FromBody] Roles rol)
        {
            if (id != rol.ro_identificador)
            {
                return BadRequest();
            }

            _contexto.Entry(rol).State = EntityState.Modified;

            try
            {
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_contexto.Roles.Any(e => e.ro_identificador == id))
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
        public async Task<IActionResult> DeleteRol(int id)
        {
            var rol = await _contexto.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            _contexto.Roles.Remove(rol);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }




    }


}

