using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    [Table("precios")]
    public class Precios
    {
        [Key]
        [Column("idpre")]
        public string idpre { get; set; }


        [Required]
        [Column("inscripcionpre")]
        public decimal inscripcionpre { get; set; }


        [Required]
        [Column("matriculapre")]
        public decimal matriculapre { get; set; }


        [Required]
        [Column("arancelpre")]
        public decimal arancelpre { get; set; }


        [Column("monedapre")]
        public string monedapre { get; set; } = "USD";


        [Column("vigente")]
        public bool vigente { get; set; } = true;


        public List<Programas>? Programas { get; set; } = new List<Programas>();

    }
}
