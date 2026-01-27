using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    public class Usuarios
    {
        [Key] public int Id { get; set; }

        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        public string EmailUsuario { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public bool ActivoUsu { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        public List<UsuarioRoles>? UsuarioRoles { get; set; }

    }
}
