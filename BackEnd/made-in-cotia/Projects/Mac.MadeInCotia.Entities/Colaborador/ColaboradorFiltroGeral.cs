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
        public DateTime? DataInicio { get; set; } = DateTime.Now;
        public DateTime? DataFim { get; set; } = DateTime.Now;
        public bool RegistroTemporal { get; set; }

    }
}
