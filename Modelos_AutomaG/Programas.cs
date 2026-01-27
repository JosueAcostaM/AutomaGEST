using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    public class Programas
    {
        [Key] public int Id { get; set; }

        [Required]
        public string NombrePrograma { get; set; }


        [Required, ForeignKey("IdCamposConocimientos")]
        public string IdCamposConocimientos { get; set; }
        public  CamposConocimientos Campo { get; set; }


        [Required, ForeignKey("IdNiveles")]
        public string IdNiveles { get; set; }
        public  Niveles Nivel { get; set; }


        [Required, ForeignKey("IdModalidades")]
         public string IdModalidades { get; set; }
         public  Modalidades Modalidad { get; set; }


        public string DuracionPrograma { get; set; }

        public string DescripcionPrograma { get; set; }

        public string EstadoPro { get; set; } = "activo";

        public  List<Precios>? Precios { get; set; }
    }
}
