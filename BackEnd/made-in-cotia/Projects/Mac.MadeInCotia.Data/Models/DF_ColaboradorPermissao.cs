using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mac.MadeInCotia.Data.Models
{
    public class DF_ColaboradorPermissao
    {
        
        [Key]
        public int Id_ColaboradorPermissao { get; set; }
        [ForeignKey("CF_Colaborador")]
        public int Id_Colaborador { get; set; }
        [ForeignKey("DF_Permissao")]
        public int Id_Permissao { get; set; }
        public bool? Fl_Ativo { get; set; }
        public DateTime? Dt_Criacao { get; set; }
        public virtual CF_Colaborador CF_Colaborador { get; set; } = null!;
        public virtual DF_Permissao DF_Permissao { get; set; } = null!;

    }
}
