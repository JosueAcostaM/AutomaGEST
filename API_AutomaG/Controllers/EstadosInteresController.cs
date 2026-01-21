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
    public class EstadosInteresController : ControllerBase
    {
        private readonly API_AutomaGContext _context;

        public EstadosInteresController(API_AutomaGContext context)
        {
            _context = context;
        }

        // GET: api/EstadosInteres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadosInteres>>> GetEstadosInteres()
        {
            return await _context.EstadosInteres.ToListAsync();
        }

        // GET: api/EstadosInteres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadosInteres>> GetEstadosInteres(int id)
        {
            var estadosInteres = await _context.EstadosInteres.FindAsync(id);

            if (estadosInteres == null)
            {
                return NotFound();
            }

            return estadosInteres;
        }

        // PUT: api/EstadosInteres/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstadosInteres(int id, EstadosInteres estadosInteres)
        {
            if (id != estadosInteres.Id)
            {
                return BadRequest();
            }

            _context.Entry(estadosInteres).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadosInteresExists(id))
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

        // POST: api/EstadosInteres
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EstadosInteres>> PostEstadosInteres(EstadosInteres estadosInteres)
        {
            _context.EstadosInteres.Add(estadosInteres);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstadosInteres", new { id = estadosInteres.Id }, estadosInteres);
        }

        // DELETE: api/EstadosInteres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstadosInteres(int id)
        {
            var estadosInteres = await _context.EstadosInteres.FindAsync(id);
            if (estadosInteres == null)
            {
                return NotFound();
            }

            _context.EstadosInteres.Remove(estadosInteres);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstadosInteresExists(int id)
        {
            return _context.EstadosInteres.Any(e => e.Id == id);
        }
    }
}
