using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    public class Conversacion
    {
        [Key] public int Id { get; set; }


        [Required, ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        public Usuario Usuario { get; set; }

        public DateTime FechaIncio { get; set; }

        public DateTime FechaFin {  get; set; }

        
        //Posible tabla
        public string Estado { get; set; }



    }
}
