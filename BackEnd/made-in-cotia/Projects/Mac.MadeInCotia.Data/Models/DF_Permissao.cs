using System.ComponentModel.DataAnnotations;

namespace Mac.MadeInCotia.Data.Models
{
    public class DF_Permissao
    {
        public DF_Permissao(int id_permissao, string nm_permissao, bool fl_ativo, DateTime dt_criacao)
        { 
            Id_Permissao = id_permissao;
            Nm_Permissao = nm_permissao;
            Fl_Ativo = fl_ativo;
            Dt_Criacao = dt_criacao;
        }

        public DF_Permissao()
        { 

        }

        [Key]
        public int Id_Permissao { get; set; }
        public string Nm_Permissao { get; set; } = null!;
        public bool? Fl_Ativo { get; set; }
        public DateTime? Dt_Criacao { get; set; }
        public virtual ICollection<DF_ColaboradorPermissao> CfColaboradorPermissao { get; set; } = new List<DF_ColaboradorPermissao>();
    }
}
