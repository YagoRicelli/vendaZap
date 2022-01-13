using System.Collections.Generic;
using System.Linq;
using VendaZap.Comum.Dominio;
using VendaZap.Usuarios.Dominio.CasosDeUso.ConsultarPorId;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.ConsultarLideres
{
    public class ConsultaLideresResponse : Response
    {
        public ConsultaLideresResponse(List<UsuarioDTO> usuario)
        {
            if (usuario == null || !usuario.Any())
            {
                return;
            }

            this.Lideres = usuario.Select(x => new LideresRetorno
            {
                Id = x.Id,
                Nome = x.Nome,
                Ativo = x.Ativo,
                Sobrenome = x.Sobrenome
            }).ToList();
        }

        public List<LideresRetorno> Lideres { get; set; }

        public class LideresRetorno
        {
            public string Id { get; set; }
            public string Nome { get; set; }
            public string Sobrenome { get; set; }
            public bool Ativo { get; set; }
        }
    }
}
