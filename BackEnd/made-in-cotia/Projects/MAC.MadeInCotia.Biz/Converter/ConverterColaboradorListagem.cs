using Mac.MadeInCotia.Data.Models;
using Mac.MadeInCotia.Entities.Colaborador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAC.MadeInCotia.Biz.Converter
{
    public static class ConverterColaboradorListagem
    {
        public static ColaboradorListagemViewModel Converter(CF_Colaborador colaborador)
        {

            CF_ColaboradorEmail email = colaborador.CF_ColaboradorEmail.FirstOrDefault(e => e.Fl_Principal);
            CF_ColaboradorTelefone telefone = colaborador.CF_ColaboradorTelefone.FirstOrDefault(t => t.Fl_Principal);
            string telefoneColaborador = telefone != null ? telefone.Ds_Numero : string.Empty;
            string emailColaborador = email != null ? email.Ds_Email : string.Empty;
            return new ColaboradorListagemViewModel(colaborador.Id_Colaborador, colaborador.Nm_Nome, colaborador.Ds_Cpf, telefoneColaborador, emailColaborador, colaborador.Dt_Criacao, colaborador.Ds_UltAlteracao);
        }

        public static List<ColaboradorListagemViewModel> Converter(List<CF_Colaborador> listaColaborador)
        {
            List<ColaboradorListagemViewModel> listaFormatada = new List<ColaboradorListagemViewModel> ();
            foreach (var item in listaColaborador)
            {
                listaFormatada.Add(Converter(item));
            }

            return listaFormatada;
        }
    }

    
}
