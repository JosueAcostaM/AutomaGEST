using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    public class Aspirantes
    {
        [Key] public int Id { get; set; }


        [Required, ForeignKey("IdContactos")]
        public string IdContactos { get; set; }
        public Contactos Contactos { get; set; }

        public string NombreAspirante { get; set; }

        public string EmailAspirante { get; set; }

        public string ProvinciaAspirante { get; set; }

        public string CiudadAspirante { get; set; }

        public string NivelInteres { get; set; }

        public string ProgramaInteres { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
