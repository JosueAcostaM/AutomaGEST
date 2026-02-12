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
    public class TiposHorariosController : ControllerBase
    {
        private readonly API_AutomaGContext _context;

        public TiposHorariosController(API_AutomaGContext context)
        {
            _context = context;
        }

        // GET: api/TiposHorarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TiposHorario>>> GetTiposHorario()
        {
            return await _context.TiposHorario.ToListAsync();
        }

        // GET: api/TiposHorarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TiposHorario>> GetTiposHorario(string id)
        {
            var tiposHorario = await _context.TiposHorario.FindAsync(id);

            if (tiposHorario == null)
            {
                return NotFound();
            }

            return tiposHorario;
        }

        // PUT: api/TiposHorarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTiposHorario(string id, TiposHorario tiposHorario)
        {
            if (id != tiposHorario.idtipo)
            {
                return BadRequest();
            }

            _context.Entry(tiposHorario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TiposHorarioExists(id))
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

        // POST: api/TiposHorarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TiposHorario>> PostTiposHorario(TiposHorario tiposHorario)
        {
            _context.TiposHorario.Add(tiposHorario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TiposHorarioExists(tiposHorario.idtipo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTiposHorario", new { id = tiposHorario.idtipo }, tiposHorario);
        }

        // DELETE: api/TiposHorarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTiposHorario(string id)
        {
            var tiposHorario = await _context.TiposHorario.FindAsync(id);
            if (tiposHorario == null)
            {
                return NotFound();
            }

            _context.TiposHorario.Remove(tiposHorario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TiposHorarioExists(string id)
        {
            return _context.TiposHorario.Any(e => e.idtipo == id);
        }
    }
}
