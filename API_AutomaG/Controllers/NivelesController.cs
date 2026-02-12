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
using Microsoft.AspNetCore.Authorization;


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
            for (int intento = 1; intento <= 5; intento++)
            {
                var ids = await _context.Niveles
                    .AsNoTracking()
                    .Where(p => p.idniv.StartsWith("NIV"))
                    .Select(p => p.idniv)
                    .ToListAsync();

                int maxNum = 0;
                foreach (var id in ids)
                {
                    if (id != null && id.StartsWith("NIV"))
                    {
                        var numStr = id.Substring(3);
                        if (int.TryParse(numStr, out int n) && n > maxNum)
                            maxNum = n;
                    }
                }

                niveles.idniv = $"NIV{maxNum + 1}";

                _context.Niveles.Add(niveles);

                try
                {
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetNiveles", new { id = niveles.idniv }, niveles);
                }
                catch (DbUpdateException ex) when (ex.InnerException is PostgresException pg && pg.SqlState == "23505")
                {
                    _context.ChangeTracker.Clear();
                    if (intento == 5) throw;
                }
            }

            return StatusCode(500, "No se pudo crear el precio.");
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
