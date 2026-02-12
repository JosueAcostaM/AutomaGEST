using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_AutomaG.Data;
using Modelos_AutomaG;
using Microsoft.AspNetCore.Authorization;

namespace API_AutomaG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AspirantesController : ControllerBase
    {
        private readonly API_AutomaGContext _context;

        public AspirantesController(API_AutomaGContext context)
        {
            _context = context;
        }

        // GET: api/Aspirantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aspirantes>>> GetAspirantes()
        {
            return await _context.Aspirantes.ToListAsync();
        }

        // GET: api/Aspirantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aspirantes>> GetAspirantes(string id)
        {
            var aspirante = await _context.Aspirantes.
                Include(a => a.Contacto).
                FirstOrDefaultAsync(a=> a.idasp==id);
                

            if (aspirante == null)
                return NotFound();

           
            return aspirante;
        }

        // PUT: api/Aspirantes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // En tu archivo: API_AutomaG\Controllers\AspirantesController.cs

        //Guardar informacion aspirante
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAspirantes(string id, Aspirantes aspirantes)
        {
            if (id != aspirantes.idasp)
            {
                return BadRequest();
            }

            var existingAspirante = await _context.Aspirantes.FindAsync(id);
            if (existingAspirante == null)
            {
                return NotFound();
            }

            // Marcar explícitamente que fecharegistro NO debe actualizarse
            _context.Entry(existingAspirante).Property(x => x.fecharegistro).IsModified = false;

            // Actualizar solo los campos necesarios
            existingAspirante.nombreasp = aspirantes.nombreasp;
            existingAspirante.apellidoasp = aspirantes.apellidoasp;
            existingAspirante.emailasp = aspirantes.emailasp;
            existingAspirante.provinciaasp = aspirantes.provinciaasp;
            existingAspirante.ciudadasp = aspirantes.ciudadasp;
            existingAspirante.programainteres = aspirantes.programainteres;
            existingAspirante.nivelinteres = aspirantes.nivelinteres;
            existingAspirante.idcon = aspirantes.idcon;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspirantesExists(id))
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
        //Cambiar Estado del Aspirante
        // PUT: api/Aspirantes/cambiar-estado/5
        [HttpPut("cambiar-estado/{id}")]
        public async Task<IActionResult> CambiarEstado(string id)
        {
            var aspirante = await _context.Aspirantes.FindAsync(id);

            if (aspirante == null)
                return NotFound();

            // Toggle simple
            aspirante.estadoasp = aspirante.estadoasp == "En revisión"
                ? "Revisado"
                : "En revisión";

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Aspirantes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Aspirantes>> PostAspirantes(Aspirantes aspirantes)
        {
            aspirantes.estadoasp= "En revisión";
            _context.Aspirantes.Add(aspirantes);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (AspirantesExists(aspirantes.idasp))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetAspirantes", new { id = aspirantes.idasp }, aspirantes);
        }

        // DELETE: api/Aspirantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAspirantes(string id)
        {
            var aspirantes = await _context.Aspirantes.FindAsync(id);
            if (aspirantes == null)
            {
                return NotFound();
            }

            _context.Aspirantes.Remove(aspirantes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AspirantesExists(string id)
        {
            return _context.Aspirantes.Any(e => e.idasp == id);
        }
    }
}
