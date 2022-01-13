using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaZap.Comum.Dominio;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.RealizarLogin
{
    public class RealizaLoginResponse : Response
    {
        public string Id { get; set; }
        public string Token { get; set; }

        public RealizaLoginResponse(string id, string token)
        {
            this.Token = token;
            this.Id = id;
        }
    }
}
