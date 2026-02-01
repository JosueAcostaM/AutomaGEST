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
    public class CamposConocimientosController : ControllerBase
    {
        private readonly API_AutomaGContext _context;

        public CamposConocimientosController(API_AutomaGContext context)
        {
            _context = context;
        }

        // GET: api/CamposConocimientos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CamposConocimiento>>> GetCamposConocimiento()
        {
            return await _context.CamposConocimiento
                .Include(c => c.Programas)
                .ToListAsync();
        }

        // GET: api/CamposConocimientos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CamposConocimiento>> GetCamposConocimiento(string id)
        {
            var campo = await _context.CamposConocimiento
                .Include(c => c.Programas)
                .FirstOrDefaultAsync(c => c.idcam == id);

            if (campo == null)
            {
                return NotFound();
            }

            return campo;
        }

        // PUT: api/CamposConocimientos/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCamposConocimiento(string id, CamposConocimiento camposConocimiento)
        {
            if (id != camposConocimiento.idcam)
            {
                return BadRequest();
            }

            _context.Entry(camposConocimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CamposConocimientoExists(id))
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

        // POST: api/CamposConocimientos
        [HttpPost]
        public async Task<ActionResult<CamposConocimiento>> PostCamposConocimiento(CamposConocimiento camposConocimiento)
        {
            _context.CamposConocimiento.Add(camposConocimiento);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetCamposConocimiento",
                new { id = camposConocimiento.idcam },
                camposConocimiento
            );
        }

        // DELETE: api/CamposConocimientos/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCamposConocimiento(string id)
        {
            var campo = await _context.CamposConocimiento.FindAsync(id);

            if (campo == null)
            {
                return NotFound();
            }

            _context.CamposConocimiento.Remove(campo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CamposConocimientoExists(string id)
        {
            return _context.CamposConocimiento.Any(e => e.idcam == id);
        }
    }
}
