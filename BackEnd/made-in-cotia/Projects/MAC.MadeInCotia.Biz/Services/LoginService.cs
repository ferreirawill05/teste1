using Mac.Common.Util.Core;
using Mac.MadeInCotia.Data.Context;
using Mac.MadeInCotia.Data.Models;
using Mac.MadeInCotia.Entities.Colaborador;
using System.Text;

namespace MAC.MadeInCotia.Biz.Services
{
    public class LoginService
    {
        private readonly MacMadeInCotiaContext _context;

        public LoginService(MacMadeInCotiaContext context)
        {
            _context = context;
        }
        
        public bool Logar(LoginViewModel loginViewModel)
        {
            CF_Colaborador? colaborador =  _context.CF_Colaborador.FirstOrDefault(c => c.Nm_Usuario == loginViewModel.Nm_Usuario);

            if (colaborador == null) return false;
            var teste = Crypt.Decrypt(colaborador.Ds_Senha);
            
            return Crypt.Decrypt(colaborador.Ds_Senha) == loginViewModel.Ds_Senha;
        }

    }
}
