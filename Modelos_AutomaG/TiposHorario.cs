using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    [Table("tipos_horario")]
    public class TiposHorario
    {
        [Key]
        [Column("idtipo")]
        public string idtipo { get; set; }


        [Required]
        [Column("nombretipo")]
        public string nombretipo { get; set; }


        public  List<Horarios>? Horarios { get; set; } = new List<Horarios>();
    }
}
