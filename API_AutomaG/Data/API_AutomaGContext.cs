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
        public DbSet<Modelos_AutomaG.Aspirantes> Aspirantes { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProgramasHorarios>()
                .HasKey(ph => new { ph.idpro, ph.idhor });

            modelBuilder.Entity<UsuarioRoles>()
                .HasKey(ur => new { ur.idusu, ur.idrol });

            modelBuilder.Entity<ProgramasHorarios>().ToTable("programas_horarios");
            modelBuilder.Entity<UsuarioRoles>().ToTable("usuario_roles");
        }

        public DbSet<Modelos_AutomaG.CamposConocimiento> CamposConocimiento { get; set; } = default!;
        public DbSet<Modelos_AutomaG.Contactos> Contactos { get; set; } = default!;
        public DbSet<Modelos_AutomaG.Horarios> Horarios { get; set; } = default!;
        public DbSet<Modelos_AutomaG.Modalidades> Modalidades { get; set; } = default!;
        public DbSet<Modelos_AutomaG.Niveles> Niveles { get; set; } = default!;
        public DbSet<Modelos_AutomaG.Precios> Precios { get; set; } = default!;
        public DbSet<Modelos_AutomaG.Programas> Programas { get; set; } = default!;
        public DbSet<Modelos_AutomaG.ProgramasHorarios> ProgramasHorarios { get; set; } = default!;
        public DbSet<Modelos_AutomaG.Roles> Roles { get; set; } = default!;
        public DbSet<Modelos_AutomaG.TiposHorario> TiposHorario { get; set; } = default!;
        public DbSet<Modelos_AutomaG.UsuarioRoles> UsuarioRoles { get; set; } = default!;
        public DbSet<Modelos_AutomaG.Usuarios> Usuarios { get; set; } = default!;

    }
}
