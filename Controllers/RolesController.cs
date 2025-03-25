using Microsoft.AspNetCore.Mvc;
using WebAPITickets.Database;
using WebAPITickets.Models;

namespace WebAPITickets.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly ContextoBD _context;

        public RolesController(ContextoBD context)
        {

            _context = context;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<Roles>>> GetRol()

        {

            return await _contexto.listaRoles.ToListAsync();

        }

    }
}
