using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_AutomaG.Data;
using Modelos_AutomaG;

namespace API_AutomaG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioRolesController : ControllerBase
    {
        private readonly API_AutomaGContext _context;

        public UsuarioRolesController(API_AutomaGContext context)
        {
            _context = context;
        }

        // GET: api/UsuarioRoles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioRoles>>> GetUsuarioRoles()
        {
            return await _context.UsuarioRoles.ToListAsync();
        }

        // GET: api/UsuarioRoles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioRoles>> GetUsuarioRoles(string id)
        {
            var usuarioRoles = await _context.UsuarioRoles.FindAsync(id);

            if (usuarioRoles == null)
            {
                return NotFound();
            }

            return usuarioRoles;
        }

        // PUT: api/UsuarioRoles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarioRoles(string id, UsuarioRoles usuarioRoles)
        {
            if (id != usuarioRoles.idusu)
            {
                return BadRequest();
            }

            _context.Entry(usuarioRoles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioRolesExists(id))
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

        // POST: api/UsuarioRoles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsuarioRoles>> PostUsuarioRoles(UsuarioRoles usuarioRoles)
        {
            // 1. Limpiar navegaciones
            usuarioRoles.Usuario = null;
            usuarioRoles.Rol = null;

            // 2. Verificar si ya existe para este usuario
            var existente = await _context.UsuarioRoles
                .FirstOrDefaultAsync(ur => ur.idusu == usuarioRoles.idusu);

            if (existente != null)
            {
                // Si ya tiene un rol, lo borramos para poner el nuevo
                _context.UsuarioRoles.Remove(existente);
                await _context.SaveChangesAsync();
            }

            _context.UsuarioRoles.Add(usuarioRoles);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarioRoles", new { id = usuarioRoles.idusu }, usuarioRoles);
        }

        // DELETE: api/UsuarioRoles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioRoles(string id)
        {
            var usuarioRoles = await _context.UsuarioRoles.FindAsync(id);
            if (usuarioRoles == null)
            {
                return NotFound();
            }

            _context.UsuarioRoles.Remove(usuarioRoles);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioRolesExists(string id)
        {
            return _context.UsuarioRoles.Any(e => e.idusu == id);
        }
    }
}
