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
    public class PostgradosController : ControllerBase
    {
        private readonly API_AutomaGContext _context;

        public PostgradosController(API_AutomaGContext context)
        {
            _context = context;
        }

        // GET: api/Postgrados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Postgrado>>> GetPostgrado()
        {
            return await _context.Postgrado.ToListAsync();
        }

        // GET: api/Postgrados/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Postgrado>> GetPostgrado(int id)
        {
            var postgrado = await _context.Postgrado.FindAsync(id);

            if (postgrado == null)
            {
                return NotFound();
            }

            return postgrado;
        }

        // PUT: api/Postgrados/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostgrado(int id, Postgrado postgrado)
        {
            if (id != postgrado.Id)
            {
                return BadRequest();
            }

            _context.Entry(postgrado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostgradoExists(id))
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

        // POST: api/Postgrados
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Postgrado>> PostPostgrado(Postgrado postgrado)
        {
            _context.Postgrado.Add(postgrado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPostgrado", new { id = postgrado.Id }, postgrado);
        }

        // DELETE: api/Postgrados/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostgrado(int id)
        {
            var postgrado = await _context.Postgrado.FindAsync(id);
            if (postgrado == null)
            {
                return NotFound();
            }

            _context.Postgrado.Remove(postgrado);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostgradoExists(int id)
        {
            return _context.Postgrado.Any(e => e.Id == id);
        }
    }
}
