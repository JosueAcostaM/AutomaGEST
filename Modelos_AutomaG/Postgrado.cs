using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    public class Postgrado
    {
        [Key] public int Id { get; set; }


        [Required, ForeignKey("Area")]
        public int IdArea { get; set; }

        public Area Area { get; set; }


        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Requisitos { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin {  get; set; }

        //posible tabla
        public string Modalidad { get; set; }

        public float Costo { get; set; }

        public bool Activo {  get; set; } = true;

    }
}
