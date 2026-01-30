using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    [Table("niveles")]
    public class Niveles
    {
        [Key]
        [Column("idniv")]
        public string idniv { get; set; }


        [Required]
        [Column("codigoniv")]
        public string codigoniv { get; set; }


        [Required]
        [Column("nombreniv")]
        public string nombreniv { get; set; }

        public List<Programas>? Programas { get; set; } = new List<Programas>();
    }
}
