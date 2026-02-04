using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    [Table("campos_conocimiento")]
    public class CamposConocimiento
    {
        [Key]
        [Column("idcam")]
        public string idcam { get; set; }


        [Required]
        [Column("codigocam")]
        public string codigocam { get; set; }


        [Required]
        [Column("nombrecam")]
        public string nombrecam { get; set; }


        [Column("descripcioncam")]
        public string? descripcioncam { get; set; }


        [Column("estadocam")]
        public string estadocam { get; set; } = "activo";
        public List<Programas>? Programas { get; set; } = new List<Programas>();

    }
}
