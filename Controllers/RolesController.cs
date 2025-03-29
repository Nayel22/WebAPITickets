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

    }
}
