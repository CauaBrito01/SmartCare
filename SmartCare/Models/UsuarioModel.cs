using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace SmartCare.Models
{
    public class UsuarioModel
    {
        [Key]
        public int ID_USUARIO { get; set; }
        public string NOME_USUARIO { get; set; }
        public DateTime DAT_NASCIMENTO { get; set; }
        public string CPF_USUARIO { get; set; }
        public int ID_ELENCO { get; set; }
        public decimal IND_VIGENTE { get; set; }
        public string? email { get; set; }
        public string? senha { get; set; }
    }
}


