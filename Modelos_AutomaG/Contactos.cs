using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    [Table("contactos")]
    public class Contactos
    {
        [Key]
        [Column("idcon")]
        public string idcon { get; set; }


        [Required]
        [Column("telefonocon")]
        public string telefonocon { get; set; }


        [Column("fechacontacto")]
        public DateTime fechacontacto { get; set; } = DateTime.Now;


        public  List<Aspirantes>? Aspirantes { get; set; } = new List<Aspirantes>();
    }
}
