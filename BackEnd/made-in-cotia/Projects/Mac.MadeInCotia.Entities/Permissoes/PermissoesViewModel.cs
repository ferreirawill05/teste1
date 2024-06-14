using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mac.MadeInCotia.Entities.Permissoes
{
    public class PermissoesViewModel
    {
        public int Id_Permissao { get; set; }
        public string Nm_Permissao { get; set; } = null!;
        public DateTime? Dt_Criacao { get; set; }
        public bool FlPrincipal { get; set; }

    }
}
