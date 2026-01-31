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
    public class ModalidadesController : ControllerBase
    {
        private readonly API_AutomaGContext _context;

        public ModalidadesController(API_AutomaGContext context)
        {
            _context = context;
        }

        // GET: api/Modalidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Modalidades>>> GetModalidades()
        {
            return await _context.Modalidades.ToListAsync();
        }

        // GET: api/Modalidades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Modalidades>> GetModalidades(string id)
        {
            var modalidades = await _context.Modalidades.FindAsync(id);

            if (modalidades == null)
            {
                return NotFound();
            }

            return modalidades;
        }

        // PUT: api/Modalidades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModalidades(string id, Modalidades modalidades)
        {
            if (id != modalidades.idmod)
            {
                return BadRequest();
            }

            _context.Entry(modalidades).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModalidadesExists(id))
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

        // POST: api/Modalidades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Modalidades>> PostModalidades(Modalidades modalidades)
        {
            _context.Modalidades.Add(modalidades);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ModalidadesExists(modalidades.idmod))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetModalidades", new { id = modalidades.idmod }, modalidades);
        }

        // DELETE: api/Modalidades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModalidades(string id)
        {
            var modalidades = await _context.Modalidades.FindAsync(id);
            if (modalidades == null)
            {
                return NotFound();
            }

            _context.Modalidades.Remove(modalidades);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModalidadesExists(string id)
        {
            return _context.Modalidades.Any(e => e.idmod == id);
        }
    }
}
