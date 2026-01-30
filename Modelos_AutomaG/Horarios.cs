using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    [Table("horarios")]
    public class Horarios
    {
        [Key]
        [Column("idhor")]
        public string idhor { get; set; }


        [Required]
        [Column("idtipo")]
        public string idtipo { get; set; }


        [Required]
        [Column("dia")]
        public string dia { get; set; }


        [Required]
        [Column("horainicio")]
        public TimeSpan horainicio { get; set; }


        [Required]
        [Column("horafin")]
        public TimeSpan horafin { get; set; }


        [ForeignKey("idtipo")]
        public  TiposHorario? TipoHorario { get; set; }

        public List<ProgramasHorarios>? ProgramasHorarios { get; set; } = new List<ProgramasHorarios>();

    }
}
