using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    public class UsuarioRoles
    {
        [Key ] public int Id { get; set; }


        [Required, ForeignKey("IdUsuarios")]
        public string IdUsuarios { get; set; }
        public Usuarios Usuarios { get; set; }



        [Required, ForeignKey("IdRoles")]
        public string IdRoles { get; set; }

        public Roles Roles { get; set; }
    }
}
