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
    public class ContactosController : ControllerBase
    {
        private readonly API_AutomaGContext _context;

        public ContactosController(API_AutomaGContext context)
        {
            _context = context;
        }

        // GET: api/Contactos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contactos>>> GetContactos()
        {
            return await _context.Contactos.ToListAsync();
        }

        // GET: api/Contactos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contactos>> GetContactos(string id)
        {
            var contactos = await _context.Contactos.FindAsync(id);

            if (contactos == null)
            {
                return NotFound();
            }

            return contactos;
        }

        // PUT: api/Contactos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactos(string id, Contactos contactos)
        {
            if (id != contactos.idcon)
            {
                return BadRequest();
            }

            _context.Entry(contactos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactosExists(id))
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

        // POST: api/Contactos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contactos>> PostContactos(Contactos contactos)
        {
            _context.Contactos.Add(contactos);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContactosExists(contactos.idcon))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetContactos", new { id = contactos.idcon }, contactos);
        }

        // DELETE: api/Contactos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactos(string id)
        {
            var contactos = await _context.Contactos.FindAsync(id);
            if (contactos == null)
            {
                return NotFound();
            }

            _context.Contactos.Remove(contactos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactosExists(string id)
        {
            return _context.Contactos.Any(e => e.idcon == id);
        }
    }
}
