using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    public class Precios
    {
        [Key] public int Id { get; set; }


        [Required, ForeignKey("IdProgramas")]
        public string IdProgramas { get; set; }
        public Programas Programa { get; set; }


        [Required]
        public decimal ValorPre { get; set; }

        public string MonedaPre { get; set; } = "USD";

        public bool Vigente { get; set; } = true;
    }
}
