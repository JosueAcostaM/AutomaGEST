using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    public class Mensajes
    {
        [Key] public int Id { get; set; }


        [Required, ForeignKey("Conversacion")]
        public int IdConversacion { get; set; }

        public Conversacion Conversacion { get; set; }


        //Usuario / Bot
        public string Emisor {  get; set; }

        public string Mensaje { get; set; }

        public DateTime FechaEnvio { get; set; }


    }
}
