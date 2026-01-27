using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    public class Conversaciones
    {
        [Key] public int Id { get; set; }


        [Required, ForeignKey("IdContactos")]
        public string IdContactos { get; set; }
        public Contactos Contactos { get; set; }

        public string EstadoConversacion { get; set; }

        public DateTime UltimaActualizacion { get; set; } = DateTime.Now;
    }
}
