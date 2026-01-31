using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    [Table("programas_horarios")]
    public class ProgramasHorarios
    {
        [Required]
        [Column("idpro")]
        public string idpro { get; set; }


        [Required]
        [Column("idhor")]
        public string idhor { get; set; }


        [ForeignKey("idpro")] public  Programas? Programa { get; set; }
        [ForeignKey("idhor")] public  Horarios? Horario { get; set; }


    }
}
