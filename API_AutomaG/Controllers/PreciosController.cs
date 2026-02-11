using API_AutomaG.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modelos_AutomaG;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpPost]
        public async Task<ActionResult<Precios>> PostPrecios(Precios precios)
        {
            for (int intento = 1; intento <= 5; intento++)
            {
                var ids = await _context.Precios
                    .AsNoTracking()
                    .Where(p => p.idpre.StartsWith("PRE"))
                    .Select(p => p.idpre)
                    .ToListAsync();

                int maxNum = 0;
                foreach (var id in ids)
                {
                    if (id != null && id.StartsWith("PRE"))
                    {
                        var numStr = id.Substring(3);
                        if (int.TryParse(numStr, out int n) && n > maxNum)
                            maxNum = n;
                    }
                }

                precios.idpre = $"PRE{maxNum + 1}";

                _context.Precios.Add(precios);

                try
                {
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetPrecios", new { id = precios.idpre }, precios);
                }
                catch (DbUpdateException ex) when (ex.InnerException is PostgresException pg && pg.SqlState == "23505")
                {
                    _context.ChangeTracker.Clear();
                    if (intento == 5) throw;
                }
            }

            return StatusCode(500, "No se pudo crear el precio.");
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
