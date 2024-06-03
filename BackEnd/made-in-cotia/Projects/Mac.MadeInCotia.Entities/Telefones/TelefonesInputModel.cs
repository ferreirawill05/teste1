using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mac.MadeInCotia.Entities.Telefones
{
    public class TelefonesInputModel
    {
        public int IdTelefone { get; set; }
        public int IdColaborador { get; set; }
        public string NmApelido { get; set; } = null!;
        public string DsNumero { get; set; } = null!;
    }
}
