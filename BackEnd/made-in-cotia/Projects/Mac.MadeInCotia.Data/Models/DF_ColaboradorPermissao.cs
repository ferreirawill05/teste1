using System.ComponentModel.DataAnnotations;

namespace Mac.MadeInCotia.Data.Models
{
    public class DF_ColaboradorPermissao
    {
        /*public DF_ColaboradorPermissao(int id_colaboradorPermissao, int id_colaborador, int id_permissao, bool fl_ativo, DateTime dt_criacao)
        {
            Id_ColaboradorPermissao = id_colaboradorPermissao;
            Id_Colaborador = id_colaborador;
            Id_Permissao = id_permissao; 
            Fl_Ativo = fl_ativo;
            Dt_Criacao = dt_criacao;
        }

        public DF_ColaboradorPermissao()
        {
            
        }*/

        [Key]
        public int Id_ColaboradorPermissao { get; set; }
        public int Id_Colaborador { get; set; }
        public int Id_Permissao { get; set; }
        public bool? Fl_Ativo { get; set; }
        public DateTime? Dt_Criacao { get; set; }

    }
}
