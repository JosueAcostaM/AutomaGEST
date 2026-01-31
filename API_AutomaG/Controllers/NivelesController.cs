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
    public class NivelesController : ControllerBase
    {
        private readonly API_AutomaGContext _context;

        public NivelesController(API_AutomaGContext context)
        {
            _context = context;
        }

        // GET: api/Niveles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Niveles>>> GetNiveles()
        {
            return await _context.Niveles.ToListAsync();
        }

        // GET: api/Niveles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Niveles>> GetNiveles(string id)
        {
            var niveles = await _context.Niveles.FindAsync(id);

            if (niveles == null)
            {
                return NotFound();
            }

            return niveles;
        }

        // PUT: api/Niveles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNiveles(string id, Niveles niveles)
        {
            if (id != niveles.idniv)
            {
                return BadRequest();
            }

            _context.Entry(niveles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NivelesExists(id))
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

        // POST: api/Niveles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Niveles>> PostNiveles(Niveles niveles)
        {
            _context.Niveles.Add(niveles);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (NivelesExists(niveles.idniv))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetNiveles", new { id = niveles.idniv }, niveles);
        }

        // DELETE: api/Niveles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNiveles(string id)
        {
            var niveles = await _context.Niveles.FindAsync(id);
            if (niveles == null)
            {
                return NotFound();
            }

            _context.Niveles.Remove(niveles);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NivelesExists(string id)
        {
            return _context.Niveles.Any(e => e.idniv == id);
        }
    }
}
