using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    public class Niveles
    {
        [Key] public int Id { get; set; }

        [Required]
        public string CodigoNivel { get; set; }

        [Required]
        public string NombreNivel { get; set; }

        public  List<Programas>? Programas { get; set; }
    }
}
