﻿using Mac.Common.Util.Core;
using Mac.MadeInCotia.Data.Context;
using Mac.MadeInCotia.Data.Models;
using Mac.MadeInCotia.Entities.Colaborador;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        
        public string Logar(LoginViewModel loginViewModel)
        {
            CF_Colaborador? colaborador =  _context.CF_Colaborador.FirstOrDefault(c => c.Nm_Usuario == loginViewModel.Nm_Usuario);

            if (colaborador == null) return string.Empty;
            var teste = Crypt.Decrypt(colaborador.Ds_Senha);
            
            if ( Crypt.Decrypt(colaborador.Ds_Senha) == loginViewModel.Ds_Senha)
            {
                return GerarToken(colaborador);
            };
            return string.Empty;
        }

        public static string GerarToken(CF_Colaborador user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("MIC@TokenAutenticacaoWill@123MIC@TokenAutenticacaoWill@123");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Nm_Nome),
                    new Claim(ClaimTypes.Role, "master"),
                    new Claim("IdColaborador", user.Id_Colaborador.ToString()),
                    new Claim("IdPermissaoColaborador", user.Id_TipoUsuario.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        //Requisições do Token
        private static ClaimsIdentity GenerateClaims(CF_Colaborador usuario)
        {
            var newClaimIdentify = new ClaimsIdentity();


            newClaimIdentify.AddClaim(new Claim(ClaimTypes.Name, usuario.Nm_Nome));

            newClaimIdentify.AddClaim(new Claim("IdColaborador", usuario.Id_Colaborador.ToString()));

            newClaimIdentify.AddClaim(new Claim("IdTipoUsuario", usuario.Id_TipoUsuario.ToString()));

            foreach (var permissao in usuario.)
            {

                if (permissao.IdPermissao == 1)
                {
                    newClaimIdentify.AddClaim(new Claim("alterar", "1"));
                }
                if (permissao.IdPermissao == 2)
                {
                    newClaimIdentify.AddClaim(new Claim("cadastrar", "2"));
                }
                if (permissao.IdPermissao == 3)
                {
                    newClaimIdentify.AddClaim(new Claim("excluir", "3"));
                }

            }
            return newClaimIdentify;

        }

    }
}
