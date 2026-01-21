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
    public class MensajesController : ControllerBase
    {
        private readonly API_AutomaGContext _context;

        public MensajesController(API_AutomaGContext context)
        {
            _context = context;
        }

        // GET: api/Mensajes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mensajes>>> GetMensajes()
        {
            return await _context.Mensajes.ToListAsync();
        }

        // GET: api/Mensajes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Mensajes>> GetMensajes(int id)
        {
            var mensajes = await _context.Mensajes.FindAsync(id);

            if (mensajes == null)
            {
                return NotFound();
            }

            return mensajes;
        }

        // PUT: api/Mensajes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMensajes(int id, Mensajes mensajes)
        {
            if (id != mensajes.Id)
            {
                return BadRequest();
            }

            _context.Entry(mensajes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MensajesExists(id))
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

        // POST: api/Mensajes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Mensajes>> PostMensajes(Mensajes mensajes)
        {
            _context.Mensajes.Add(mensajes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMensajes", new { id = mensajes.Id }, mensajes);
        }

        // DELETE: api/Mensajes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMensajes(int id)
        {
            var mensajes = await _context.Mensajes.FindAsync(id);
            if (mensajes == null)
            {
                return NotFound();
            }

            _context.Mensajes.Remove(mensajes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MensajesExists(int id)
        {
            return _context.Mensajes.Any(e => e.Id == id);
        }
    }
}
