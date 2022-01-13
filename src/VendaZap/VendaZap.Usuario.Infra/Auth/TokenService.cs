using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VendaZap.Comum;
using VendaZap.Comum.Configuracoes;
using VendaZap.Usuarios.Dominio.CasosDeUso.RealizarLogin;
using VendaZap.Usuarios.Dominio.Servicos.Auth;

namespace VendaZap.Usuarios.Infra.Auth
{
    public class TokenService : ITokenService
    {
        private readonly IOptions<ConfiguracaoAppSettings> configuracoes;
       
        public TokenService(IOptions<ConfiguracaoAppSettings> configuracoes)
        {
            this.configuracoes = configuracoes;
        }

        public string GerarToken(UsuarioLogado usuarioLogado)
        {
            var key = Encoding.ASCII.GetBytes(this.configuracoes.Value.ChaveJWT);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioLogado.Id),
                    new Claim(ClaimTypes.Role, usuarioLogado.Tipo.Description()),
                }),

                Expires = DateTime.Now.AddMonths(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
