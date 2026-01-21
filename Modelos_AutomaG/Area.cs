using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    public class Area
    {
        [Key] public int Id { get; set; }

        public string NombreArea { get; set; }

        public string Descripcion { get; set; }
    }
}
