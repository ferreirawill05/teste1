using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mac.MadeInCotia.Entities.Colaborador
{
    public class ColaboradorFiltroGeral
    {
        public string? Busca { get; set; }
        public DateTime? DataInicio { get; set; } = DateTime.Now.AddDays(-30);
        public DateTime? DataFim { get; set; } = DateTime.Now;
    }
}
