using System.ComponentModel.DataAnnotations;

namespace Mac.MadeInCotia.Data.Models
{
    public class CF_ColaboradorTelefone
    {
        public CF_ColaboradorTelefone(int id_colaborador, string nm_apelido, string ds_numero, bool fl_principal, bool fl_ativo, DateTime dt_criacao )
        {

            Id_Colaborador = id_colaborador;
            Nm_Apelido = nm_apelido;
            Ds_Numero = ds_numero;
            Fl_Principal = fl_principal;
            Fl_Ativo = fl_ativo;
            Dt_Criacao = dt_criacao;
        }

        public CF_ColaboradorTelefone()
        {
            
        }

        [Key]
        public int Id_Telefone { get; set; }
        public int Id_Colaborador { get; set; }
        public string Nm_Apelido { get; set; } = null!;
        public string Ds_Numero { get; set; } = null!;
        public bool Fl_Principal { get; set; }
        public bool Fl_Ativo { get; set; }
        public DateTime? Dt_Criacao { get; set; }
        public DateTime? Dt_UltAlteracao { get; set; }
        public string Ds_UltAlteracao { get; set; }


    }
}
