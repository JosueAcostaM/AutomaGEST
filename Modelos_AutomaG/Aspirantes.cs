using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    [Table("aspirantes")]
    public class Aspirantes
    {
        [Key]
        [Column("idasp")]
        public string idasp { get; set; }


        [Required]
        [Column("idcon")]
        public string idcon { get; set; }


        [Column("nombreasp")]
        public string? nombreasp { get; set; }


        [Column("apellidoasp")]
        public string? apellidoasp { get; set; }


        [Column("emailasp")]
        public string? emailasp { get; set; }


        [Column("provinciaasp")]
        public string? provinciaasp { get; set; }


        [Column("ciudadasp")]
        public string? ciudadasp { get; set; }


        [Column("nivelinteres")]
        public string? nivelinteres { get; set; }

        [Column("estadoasp")]
        public string? estadoasp { get; set; } ="En revisión";

        [Column("programainteres")]
        public string? programainteres { get; set; }


        [Column("fecharegistro")]
        public DateTime fecharegistro { get; set; } = DateTime.Now;


        [ForeignKey("idcon")]
        public  Contactos? Contacto { get; set; }
    }
}
