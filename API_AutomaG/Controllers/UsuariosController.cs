using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_AutomaG.Data;
using Modelos_AutomaG;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

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
            return await _context.Usuarios
            .Include(u => u.UsuarioRoles)
            .ThenInclude(ur => ur.Rol)
            .ToListAsync();
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuarios>> GetUsuarios(string id)
        {
            var usuarios = await _context.Usuarios
            .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
            .FirstOrDefaultAsync(u => u.idusu == id);

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

            // Actualizar campos básicos
            existente.nombreusu = usuarios.nombreusu;
            existente.emailusu = usuarios.emailusu;
            existente.activousu = usuarios.activousu;

            if (usuarios.passwordhash == "vacio")
            {
                _context.Entry(existente).Property(x => x.passwordhash).IsModified = false;
                Console.WriteLine("contraseña nulla entro en la condicion");
            }
            // Gestión de contraseña
            else
            {
                existente.passwordhash = BCrypt.Net.BCrypt.HashPassword(usuarios.passwordhash);
                Console.WriteLine("contraseña escrita cambiada" + usuarios.passwordhash);
            }

                await _context.SaveChangesAsync();
            return NoContent();
        }




        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Usuarios>> PostUsuarios(Usuarios usuarios)
        {
            var ultimoId = await _context.Usuarios
                .OrderByDescending(u => u.idusu)
                .Select(u => u.idusu)
                .FirstOrDefaultAsync();

            int nuevoNumero = 1;
            if (!string.IsNullOrEmpty(ultimoId))
            {
                nuevoNumero = int.Parse(ultimoId.Replace("USU", "")) + 1;
            }
            usuarios.idusu = $"USU{nuevoNumero}";
            //Hash de password
            if (usuarios.passwordhash != "vacio") { 
            usuarios.passwordhash = BCrypt.Net.BCrypt.HashPassword(usuarios.passwordhash);

            // FIX POSTGRES: Aseguramos que la fecha sea UTC para evitar el error 500
            usuarios.fechacreacion = DateTime.UtcNow;
            }
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
