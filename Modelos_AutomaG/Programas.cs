using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    [Table("programas")]
    public class Programas
    {
        [Key]
        [Column("idpro")]
        public string idpro { get; set; }


        [Required]
        [Column("nombrepro")]
        public string nombrepro { get; set; }


        [Required]
        [Column("idcam")]
        public string idcam { get; set; }


        [Required]
        [Column("idniv")]
        public string idniv { get; set; }


        [Required]
        [Column("idmod")]
        public string idmod { get; set; }


        [Required]
        [Column("idpre")]
        public string idpre { get; set; }


        [Column("duracionpro")]
        public string? duracionpro { get; set; }


        [Column("descripcionpro")]
        public string? descripcionpro { get; set; }


        [Column("estadopro")]
        public string estadopro { get; set; } = "activo";

        [JsonIgnore]
        [ForeignKey("idcam")] public  CamposConocimiento? Campo { get; set; }
        [JsonIgnore]
        [ForeignKey("idniv")] public  Niveles? Nivel { get; set; }
        [JsonIgnore]
        [ForeignKey("idmod")] public  Modalidades? Modalidad { get; set; }
        [JsonIgnore]
        [ForeignKey("idpre")] public  Precios? Precio { get; set; }

        public  List<ProgramasHorarios>? ProgramasHorarios { get; set; } = new List<ProgramasHorarios>();
    }
}
