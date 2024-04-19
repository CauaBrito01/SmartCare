using System.ComponentModel.DataAnnotations;

namespace SmartCare.Models
{
    public class CalendarioModel
    {
        [Key]
        public int ID_CALENDARIO { get; set; }
        public int ID_USUARIO { get; set; }
        public int ID_PROFISSIONAL_RESPONSAVEL { get; set; }
        public DateTime DIA_EVENTO { get; set; }
        public string TITULO_EVENTO { get; set; }
        public string DESCRICAO_EVENTO { get; set; }
    }
}
 		