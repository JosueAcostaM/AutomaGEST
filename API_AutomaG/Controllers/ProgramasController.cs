using API_AutomaG.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos_AutomaG;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpPost]
        public async Task<ActionResult<Programas>> PostProgramas(Programas programas)
        {
            // (Opcional) estado por defecto
            if (string.IsNullOrWhiteSpace(programas.estadopro))
                programas.estadopro = "activo";

            // ✅ Reintento por si 2 personas crean a la vez y choca el ID (23505)
            for (int intento = 1; intento <= 5; intento++)
            {
                // Traer solo ids a memoria (evita el error de traducción)
                var ids = await _context.Programas
                    .AsNoTracking()
                    .Where(p => p.idpro.StartsWith("PRO"))
                    .Select(p => p.idpro)
                    .ToListAsync();

                int maxNum = 0;
                foreach (var id in ids)
                {
                    if (id != null && id.StartsWith("PRO"))
                    {
                        var numStr = id.Substring(3); // "1", "10", "100"
                        if (int.TryParse(numStr, out int n) && n > maxNum)
                            maxNum = n;
                    }
                }

                programas.idpro = $"PRO{maxNum + 1}";

                _context.Programas.Add(programas);

                try
                {
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetProgramas", new { id = programas.idpro }, programas);
                }
                catch (DbUpdateException ex) when (ex.InnerException is PostgresException pg && pg.SqlState == "23505")
                {
                    // ID duplicado por concurrencia, reintentar
                    _context.ChangeTracker.Clear();
                    if (intento == 5) throw; // ya reintentamos suficiente
                }
            }

            return StatusCode(500, "No se pudo crear el programa.");
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
