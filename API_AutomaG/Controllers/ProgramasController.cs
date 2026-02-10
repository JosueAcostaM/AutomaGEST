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
    public class ProgramasController : ControllerBase
    {
        private readonly API_AutomaGContext _context;

        public ProgramasController(API_AutomaGContext context)
        {
            _context = context;
        }

        // GET: api/Programas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Programas>>> GetProgramas()
        {
            return await _context.Programas.ToListAsync();
        }

        // GET: api/Programas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Programas>> GetProgramas(string id)
        {
            var programas = await _context.Programas
                .Include(p => p.Campo)
                .Include(p => p.Nivel)
                .Include(p => p.Modalidad)
                .Include(p => p.Precio)
                .Include(p => p.ProgramasHorarios)       
                    .ThenInclude(ph => ph.Horario)   
                .FirstOrDefaultAsync(m => m.idpro == id);

            if (programas == null)
            {
                return NotFound();
            }

            return programas;
        }

        // PUT: api/Programas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgramas(string id, Programas programas)
        {
            if (id != programas.idpro)
            {
                return BadRequest();
            }

            _context.Entry(programas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramasExists(id))
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

        // POST: api/Programas
        [HttpPost]
        public async Task<ActionResult<Programas>> PostProgramas(Programas programas)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Generar ID secuencial PRO1, PRO2, etc.
            var maxId = await _context.Programas
                .Where(p => p.idpro.StartsWith("PRO"))
                .Select(p => p.idpro)
                .ToListAsync();

            int maxNum = 0;
            foreach (var id in maxId)
            {
                if (int.TryParse(id.Replace("PRO", ""), out int num) && num > maxNum)
                    maxNum = num;
            }

            programas.idpro = $"PRO{maxNum + 1}";

            if (string.IsNullOrWhiteSpace(programas.estadopro))
                programas.estadopro = "activo";

            _context.Programas.Add(programas);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProgramasExists(programas.idpro))
                    return Conflict();
                throw;
            }

            return CreatedAtAction("GetProgramas", new { id = programas.idpro }, programas);
        }


        // DELETE: api/Programas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgramas(string id)
        {
            var programas = await _context.Programas.FindAsync(id);
            if (programas == null)
            {
                return NotFound();
            }

            _context.Programas.Remove(programas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgramasExists(string id)
        {
            return _context.Programas.Any(e => e.idpro == id);
        }
        //Cambiar estado
        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(string id)
        {
            var programa = await _context.Programas.FindAsync(id);

            if (programa == null)
                return NotFound();

            // Alternar estado
            programa.estadopro = programa.estadopro == "activo"
                ? "inactivo"
                : "activo";

            await _context.SaveChangesAsync();

            return Ok(new
            {
                id = programa.idpro,
                nuevoEstado = programa.estadopro
            });
        }

    }
}
