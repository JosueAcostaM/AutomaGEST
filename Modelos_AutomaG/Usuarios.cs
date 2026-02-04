using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Modelos_AutomaG
{
    [Table("usuarios")]
    public class Usuarios
    {
        [Key]
        [Column("idusu")]
        [JsonIgnore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? idusu { get; set; }


        [Required]
        [Column("nombreusu")]
        public string nombreusu { get; set; }


        [Required]
        [Column("emailusu")]
        public string emailusu { get; set; }


        [Required]
        [Column("passwordhash")]
        public string passwordhash { get; set; }


        [Column("activousu")]
        public bool activousu { get; set; } = true;


        [Column("fechacreacion")]
        public DateTime fechacreacion { get; set; } = DateTime.Now;


        public  List<UsuarioRoles>? UsuarioRoles { get; set; } = new List<UsuarioRoles>();
    }
}
