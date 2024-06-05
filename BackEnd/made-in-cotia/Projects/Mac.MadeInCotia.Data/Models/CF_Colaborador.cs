using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mac.MadeInCotia.Data.Models
{
    public class CF_Colaborador
    {
        public CF_Colaborador(int id_TipoUsuario, string nm_Nome, string ds_Cpf, string nm_Usuario, string ds_Senha, bool fl_Ativo, DateTime? dt_Criacao)
        {
            Id_TipoUsuario = id_TipoUsuario;
            Nm_Nome = nm_Nome;
            Ds_Cpf = ds_Cpf;
            Nm_Usuario = nm_Usuario;
            Ds_Senha = ds_Senha;
            Fl_Ativo = fl_Ativo;
            Dt_Criacao = dt_Criacao;
        }

        public CF_Colaborador()
        {
            
        }

        [Key]
        public int Id_Colaborador { get; set; }
        [ForeignKey("CF_ColaboradorTipoUsuario")]
        public int Id_TipoUsuario { get; set; }
        public string Nm_Nome { get; set; }
        public string Ds_Cpf { get; set; } 
        public string Nm_Usuario { get; set; } 
        public string Ds_Senha { get; set; }
        public bool? Fl_Ativo { get; set; }
        public DateTime? Dt_Criacao { get; set; }
        public DateTime? Dt_UltAlteracao { get; set; }
        public string? Ds_UltAlteracao { get; set; }
        public virtual CF_ColaboradorTipoUsuario CF_ColaboradorTipoUsuario { get; set; }
        public virtual ICollection<CF_ColaboradorEmail> CF_ColaboradorEmail { get; set; }
        public virtual ICollection<CF_ColaboradorTelefone> CF_ColaboradorTelefone { get; set; }
    }
} 