using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mac.MadeInCotia.Entities.Filtro
{
    internal class FiltroDataViewData
    {
        public int page { get; set; }
        public int PageSize { get; set; }
        public DateTime dataInicial { get; set; }
        public DateTime dataFinal { get; set; }
        public bool registroTemporal { get; set; }
        public string pesquisa { get; set; }
    }
}
