using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using app.Enums;

namespace app.Models
{
    [Table("Notificacao")]
    public class Notificacao
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DataNotificacao { get; set; }

        [Required]
        public TipoNotificacao TipoNotificacao { get; set; }

        [Required]
        public bool IsLida { get; set; }

        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario Usuario { get; set; }
    }
}
