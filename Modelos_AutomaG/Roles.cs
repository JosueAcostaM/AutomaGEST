using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    [Table("roles")]
    public class Roles
    {
        [Key]
        [Column("idrol")]
        public string idrol { get; set; }

        [Required]
        [Column("codigorol")]
        public string codigorol { get; set; }

        [Required]
        [Column("nombrerol")]
        public string nombrerol { get; set; }

        public List<UsuarioRoles>? UsuarioRoles { get; set; } = new List<UsuarioRoles>();

    }
}
