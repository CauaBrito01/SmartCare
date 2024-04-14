using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCare.Models
{
    public class ProfissionalModel
    {
        [Key]
        public int ID_PROF { get; set; }
        public string NOME_PROFISSIONAL { get; set; }
        public DateTime DAT_NASCIMENTO { get; set; }
        public string CPF_PROFISSIONAL { get; set; }
        public int ID_ELENCO { get; set; }
        public decimal IND_VIGENTE { get; set; }
        public decimal IND_TREINADOR { get; set; }
        public decimal IND_COMISSAO { get; set; }
        public string email { get; set; }
        public string senha { get; set; }

    }
}
