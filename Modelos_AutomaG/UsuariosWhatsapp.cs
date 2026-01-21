using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    public class UsuariosWhatsapp
    {
        [Key] public int Id { get; set; }

        public string NumeroWhatsapp { get; set; }

        public string NombreDetectado { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
