using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    public class CamposConocimientos
    {
        [Key] public int id { get; set; }

        [Required]
        public string CodigoCampo { get; set; }

        [Required]
        public string NombreCampoConoc {  get; set; }
        
        public string DescipcionCampo { get; set; } = "activo";

        public List<Programas>? Programas { get; set; }

    }
}
