using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    public class Usuario
    {
        [Key] public int Id { get; set; }

        public string Nombres { get; set; }

        public string Apellidos { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        //posible tabla
        public string Rol {  get; set; }

        public bool Activo { get; set; }
    }
}
