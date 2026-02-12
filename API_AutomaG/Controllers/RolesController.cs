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
    public class RolesController : ControllerBase
    {
        private readonly API_AutomaGContext _context;

        public RolesController(API_AutomaGContext context)
        {
            _context = context;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Roles>>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Roles>> GetRoles(string id)
        {
            var roles = await _context.Roles.FindAsync(id);

            if (roles == null)
            {
                return NotFound();
            }

            return roles;
        }

        // PUT: api/Roles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoles(string id, Roles roles)
        {
            if (id != roles.idrol)
            {
                return BadRequest();
            }

            _context.Entry(roles).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RolesExists(id))
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

        // POST: api/Roles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Roles>> PostRoles(Roles roles)
        {
            for (int intento = 1; intento <= 5; intento++)
            {
                var ids = await _context.Roles
                    .AsNoTracking()
                    .Where(p => p.idrol.StartsWith("ROL"))
                    .Select(p => p.idrol)
                    .ToListAsync();

                int maxNum = 0;
                foreach (var id in ids)
                {
                    if (id != null && id.StartsWith("ROL"))
                    {
                        var numStr = id.Substring(3);
                        if (int.TryParse(numStr, out int n) && n > maxNum)
                            maxNum = n;
                    }
                }

                roles.idrol = $"ROL{maxNum + 1}";

                _context.Roles.Add(roles);

                try
                {
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetRoles", new { id = roles.idrol }, roles);
                }
                catch (DbUpdateException ex) when (ex.InnerException is PostgresException pg && pg.SqlState == "23505")
                {
                    _context.ChangeTracker.Clear();
                    if (intento == 5) throw;
                }
            }

            return StatusCode(500, "No se pudo crear el precio.");
        }

            // DELETE: api/Roles/ROL1
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteRoles(string id)
            {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
                return NotFound();

            // 1) Borrar relaciones en usuario_roles
                var rels = await _context.Set<UsuarioRoles>()
                    .Where(x => x.idrol == id)
                    .ToListAsync();

                if (rels.Count > 0)
                    _context.Set<UsuarioRoles>().RemoveRange(rels);

                // 2) Borrar el rol
                _context.Roles.Remove(rol);

             await _context.SaveChangesAsync();
                return NoContent();
            }


        private bool RolesExists(string id)
        {
            return _context.Roles.Any(e => e.idrol == id);
        }
    }
}
