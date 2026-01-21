using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Modelos_AutomaG;

namespace API_AutomaG.Data
{
    public class API_AutomaGContext : DbContext
    {
        public API_AutomaGContext (DbContextOptions<API_AutomaGContext> options)
            : base(options)
        {
        }

        public DbSet<Modelos_AutomaG.Usuario> Usuario { get; set; } = default!;
        public DbSet<Modelos_AutomaG.Area> Area { get; set; } = default!;
        public DbSet<Modelos_AutomaG.Conversacion> Conversacion { get; set; } = default!;
        public DbSet<Modelos_AutomaG.EstadosInteres> EstadosInteres { get; set; } = default!;
        public DbSet<Modelos_AutomaG.Interesado> Interesado { get; set; } = default!;
        public DbSet<Modelos_AutomaG.Mensajes> Mensajes { get; set; } = default!;
        public DbSet<Modelos_AutomaG.Postgrado> Postgrado { get; set; } = default!;
    }
}
