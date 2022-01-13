using System.Collections.Generic;
using System.Linq;
using VendaZap.Comum.Dominio;
using VendaZap.Usuarios.Dominio.CasosDeUso.Chapas.Consultar;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Consultar;
using VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.ConsultarPorId;
using VendaZap.Usuarios.Dominio.Enumeradores;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.ConsultarPorId
{
    public class ConsultaUsuarioPorIdResponse : Response
    {
        public ConsultaUsuarioPorIdResponse(UsuarioDTO usuario)
        {
            this.Id = usuario.Id;
            this.Nome = usuario.Nome;
            this.Sobrenome = usuario.Sobrenome;
            this.Tipo = usuario.Tipo;
            this.Lider = usuario.Lider;
            this.Chapa = usuario.Chapa;

            if (usuario.Telefones != null && usuario.Telefones.Any())
            {
                this.Telefones = usuario.Telefones.Select(x => new TelefoneDTO
                {
                    Id = x.Id,
                    DDD = x.DDD,
                    Numero = x.Numero
                }).ToList();
            }
        }

        public string Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public ETipoUsuario Tipo { get; set; }
        public List<TelefoneDTO> Telefones { get; set; }
        public UsuarioLiderDTO Lider { get; set; }
        public ChapaDTO Chapa { get; set; }

    }
}
