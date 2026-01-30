using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    [Table("usuario_roles")]
    public class UsuarioRoles
    {
        [Required]
        [Column("idusu")]
        public string idusu { get; set; }


        [Required]
        [Column("idrol")]
        public string idrol { get; set; }


        [ForeignKey("idusu")] public  Usuarios? Usuario { get; set; }
        [ForeignKey("idrol")] public  Roles? Rol { get; set; }
    }
}
