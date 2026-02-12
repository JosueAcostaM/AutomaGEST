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
    public class ProgramasHorariosController : ControllerBase
    {
        private readonly API_AutomaGContext _context;

        public ProgramasHorariosController(API_AutomaGContext context)
        {
            _context = context;
        }

        // GET: api/ProgramasHorarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProgramasHorarios>>> GetProgramasHorarios()
        {
            return await _context.ProgramasHorarios.ToListAsync();
        }

        // GET: api/ProgramasHorarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProgramasHorarios>> GetProgramasHorarios(string id)
        {
            var programasHorarios = await _context.ProgramasHorarios.FindAsync(id);

            if (programasHorarios == null)
            {
                return NotFound();
            }

            return programasHorarios;
        }

        // PUT: api/ProgramasHorarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgramasHorarios(string id, ProgramasHorarios programasHorarios)
        {
            if (id != programasHorarios.idpro)
            {
                return BadRequest();
            }

            _context.Entry(programasHorarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgramasHorariosExists(id))
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

        // POST: api/ProgramasHorarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProgramasHorarios>> PostProgramasHorarios(ProgramasHorarios programasHorarios)
        {
            _context.ProgramasHorarios.Add(programasHorarios);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProgramasHorariosExists(programasHorarios.idpro))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProgramasHorarios", new { id = programasHorarios.idpro }, programasHorarios);
        }

        // DELETE: api/ProgramasHorarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgramasHorarios(string id)
        {
            var programasHorarios = await _context.ProgramasHorarios.FindAsync(id);
            if (programasHorarios == null)
            {
                return NotFound();
            }

            _context.ProgramasHorarios.Remove(programasHorarios);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProgramasHorariosExists(string id)
        {
            return _context.ProgramasHorarios.Any(e => e.idpro == id);
        }
    }
}
