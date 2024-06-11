using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mac.MadeInCotia.Entities.Colaborador
{
    public class ColaboradorListagemViewModel
    {
        public ColaboradorListagemViewModel(int idColaborador, string nome, string cpf, string telefonePrincipal, string emailPrincipal, DateTime? dtCriacao, string nsCriacao)
        {
            IdColaborador = idColaborador;
            Nome = nome;
            CPF = cpf;
            TelefonePrincipal = telefonePrincipal;
            EmailPrincipal = emailPrincipal;
            DtCriacao = dtCriacao;
            NsCriacao = nsCriacao;
        }

        public int IdColaborador {  get; set; }
        public string Nome { get; set; } = null!;
        public string CPF { get; set; } = null!;
        public string TelefonePrincipal { get; set; } = null!;
        public string EmailPrincipal { get; set; } = null!;
        public DateTime? DtCriacao { get; set;}
        public string NsCriacao { get; set;}
    }
}
