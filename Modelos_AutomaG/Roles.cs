using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    public class Roles
    {
        [Key] public int Id { get; set; }

        [Required]
        public string CodigoRol { get; set; }


        [Required]
        public string NombreRol { get; set; }

        public List <UsuarioRoles>? UsuarioRoles { get; set; }
    }
}
