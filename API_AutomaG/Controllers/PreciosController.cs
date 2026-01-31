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
    public class PreciosController : ControllerBase
    {
        private readonly API_AutomaGContext _context;

        public PreciosController(API_AutomaGContext context)
        {
            _context = context;
        }

        // GET: api/Precios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Precios>>> GetPrecios()
        {
            return await _context.Precios.ToListAsync();
        }

        // GET: api/Precios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Precios>> GetPrecios(string id)
        {
            var precios = await _context.Precios.FindAsync(id);

            if (precios == null)
            {
                return NotFound();
            }

            return precios;
        }

        // PUT: api/Precios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPrecios(string id, Precios precios)
        {
            if (id != precios.idpre)
            {
                return BadRequest();
            }

            _context.Entry(precios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreciosExists(id))
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

        // POST: api/Precios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Precios>> PostPrecios(Precios precios)
        {
            _context.Precios.Add(precios);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PreciosExists(precios.idpre))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPrecios", new { id = precios.idpre }, precios);
        }

        // DELETE: api/Precios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrecios(string id)
        {
            var precios = await _context.Precios.FindAsync(id);
            if (precios == null)
            {
                return NotFound();
            }

            _context.Precios.Remove(precios);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PreciosExists(string id)
        {
            return _context.Precios.Any(e => e.idpre == id);
        }
    }
}
