using System.ComponentModel.DataAnnotations;

namespace Mac.MadeInCotia.Data.Models
{
    public class CF_ColaboradorTipoUsuario
    {
        [Key] 
        public int Id_TipoUsuario { get; set; }
        public string Nm_TipoUsuario { get; set; } = null!;
        public bool Fl_Ativo { get; set; }
        public DateTime Dt_Criacao { get; set; }
        public DateTime? Dt_UltAlteracao { get; set; }
        public string? Ds_UltAlteracao { get; set; }
        public virtual ICollection<CF_Colaborador> CF_Colaborador { get; set; }

    }
}
