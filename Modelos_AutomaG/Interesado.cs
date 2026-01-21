using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace Modelos_AutomaG
{
    public class Interesado
    {
        [Key ] public int Id { get; set; }


        [Required, ForeignKey("Usuario")]

        public int IdUsuario { get; set; }

        public Usuario Usuario { get; set; }


        [Required, ForeignKey("Postgrado")]

        public int IdPostgrado { get; set; }

        public Postgrado Postgrado { get; set; }



        [Required, ForeignKey("EstadosInteres")]
        
        public int IdEstadosInteres {  get; set; }

        public EstadosInteres EstadosInteres { get; set; }


        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Cedula { get; set; }

        public string Correo { get; set; }

        public string NumeroTelefono { get; set; }

        public bool ExperienciaLaboral { get; set; }

        public int AniosExperiencia { get; set; }

        public string AreaExperiencia { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string Observaciones { get; set; }







    }
}
