using System.ComponentModel.DataAnnotations;

namespace SmartCare.Models
{
    public class DietaUsuarioModel
    {
        [Key]
        public int ID_DIETA { get; set; }
        public int ID_USUARIO { get; set; }
        [Required]
        public string TITULO_DIETA { get; set; }
        [Required]
        public string DESCRICAO_DIETA { get; set; }
        [Required]
        public DateTime HORA_DIETA { get; set; }
        //teste
    }
}
