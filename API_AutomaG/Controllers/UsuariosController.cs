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
    public class UsuariosController : ControllerBase
    {
        private readonly API_AutomaGContext _context;

        public UsuariosController(API_AutomaGContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuarios>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetUsuarios(string id)
        {
            var usuarios = await _context.Usuarios.FindAsync(id);

            if (usuarios == null)
            {
                return NotFound();
            }

            return usuarios;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuarios(string id, Usuarios usuarios)
        {
            if (id != usuarios.idusu)
                return BadRequest("Id no coincide.");

            var existente = await _context.Usuarios.FindAsync(id);
            if (existente == null)
                return NotFound();

            // Actualizar SOLO campos editables
            existente.nombreusu = usuarios.nombreusu;
            existente.emailusu = usuarios.emailusu;
            existente.activousu = usuarios.activousu;

            // Contraseña:
            // si llega "" => no cambiar
            // si llega texto => hashear y cambiar
            if (!string.IsNullOrWhiteSpace(usuarios.passwordhash))
            {
                existente.passwordhash = BCrypt.Net.BCrypt.HashPassword(usuarios.passwordhash);
            }

            // NO tocar fechacreacion

            await _context.SaveChangesAsync();
            return NoContent();
        }




        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuarios>> PostUsuarios(Usuarios usuarios)
        {
            // 🔹 Generar ID tipo USU1, USU2...
            var ultimoId = await _context.Usuarios
                .OrderByDescending(u => u.idusu)
                .Select(u => u.idusu)
                .FirstOrDefaultAsync();

            int nuevoNumero = 1;

            if (!string.IsNullOrEmpty(ultimoId))
            {
                // USU15 -> 15
                nuevoNumero = int.Parse(ultimoId.Replace("USU", "")) + 1;
            }

            usuarios.idusu = $"USU{nuevoNumero}";

            // 🔹 Hash de password
            usuarios.passwordhash = BCrypt.Net.BCrypt.HashPassword(usuarios.passwordhash);

            _context.Usuarios.Add(usuarios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsuarios", new { id = usuarios.idusu }, usuarios);
        }


        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarios(string id)
        {
            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuarios);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuariosExists(string id)
        {
            return _context.Usuarios.Any(e => e.idusu == id);
        }
    }
}
