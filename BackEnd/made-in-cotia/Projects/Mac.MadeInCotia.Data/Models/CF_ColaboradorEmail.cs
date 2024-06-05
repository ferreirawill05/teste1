using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mac.MadeInCotia.Data.Models
{
    public class CF_ColaboradorEmail
    {
        public CF_ColaboradorEmail(string ds_email, int id_colaborador, bool fl_principal, bool fl_ativo, DateTime? dt_criacao)
        {
            Id_colaborador = id_colaborador;
            Ds_Email = ds_email;
            Fl_Principal = fl_principal;
            Fl_Ativo = fl_ativo;
            Dt_Criacao = dt_criacao;
        }

        public CF_ColaboradorEmail()
        {
            
        }

        [Key]
        public int Id_Email { get; set; }
        [ForeignKey("CF_Colaborador")]
        public int Id_colaborador { get; set; }
        public string Ds_Email { get; set; }
        public bool Fl_Principal { get; set; }
        public bool? Fl_Ativo { get; set; }
        public DateTime? Dt_Criacao { get; set; }
        public DateTime? Dt_UltAlteracao { get; set; }
        public string? Ds_UltAlteracao { get; set; }
        public virtual CF_Colaborador CF_Colaborador { get; set; }
    }
}
