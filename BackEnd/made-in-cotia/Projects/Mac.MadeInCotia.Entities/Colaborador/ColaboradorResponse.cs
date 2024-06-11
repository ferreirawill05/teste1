using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mac.MadeInCotia.Entities.Colaborador
{
    public class ColaboradorResponse
    {
        public ColaboradorResponse(List<ColaboradorListagemViewModel> colaboradores, int quantidade)
        {
            Colaboradores = colaboradores;
            Quantidade = quantidade;
        }

        public List<ColaboradorListagemViewModel> Colaboradores { get; set; }
        public int Quantidade { get; set; }
    }
}
