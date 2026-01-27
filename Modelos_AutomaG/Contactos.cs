using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    public class Contactos
    {
        [Key] public int Id { get; set; }

        [Required]
        public string TelefonoContacto { get; set; }

        public DateTime FechaContacto { get; set; } = DateTime.Now;

        public  List<Conversaciones>? Conversaciones { get; set; }
        public List<Aspirantes>? Aspirantes { get; set; }
    }
}
