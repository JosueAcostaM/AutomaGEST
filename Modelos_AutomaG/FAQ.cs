using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    public class FAQ
    {
        [Key] public int Id { get; set; }

        public string CategoriaFaq { get; set; }

        public string PreguntaClave { get; set; }

        public string Respuesta { get; set; }
    }
}
