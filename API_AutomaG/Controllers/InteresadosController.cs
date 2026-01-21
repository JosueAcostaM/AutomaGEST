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
    public class InteresadosController : ControllerBase
    {
        private readonly API_AutomaGContext _context;

        public InteresadosController(API_AutomaGContext context)
        {
            _context = context;
        }

        // GET: api/Interesados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Interesado>>> GetInteresado()
        {
            return await _context.Interesado.ToListAsync();
        }

        // GET: api/Interesados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Interesado>> GetInteresado(int id)
        {
            var interesado = await _context.Interesado.FindAsync(id);

            if (interesado == null)
            {
                return NotFound();
            }

            return interesado;
        }

        // PUT: api/Interesados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInteresado(int id, Interesado interesado)
        {
            if (id != interesado.Id)
            {
                return BadRequest();
            }

            _context.Entry(interesado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InteresadoExists(id))
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

        // POST: api/Interesados
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Interesado>> PostInteresado(Interesado interesado)
        {
            _context.Interesado.Add(interesado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInteresado", new { id = interesado.Id }, interesado);
        }

        // DELETE: api/Interesados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInteresado(int id)
        {
            var interesado = await _context.Interesado.FindAsync(id);
            if (interesado == null)
            {
                return NotFound();
            }

            _context.Interesado.Remove(interesado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InteresadoExists(int id)
        {
            return _context.Interesado.Any(e => e.Id == id);
        }
    }
}
