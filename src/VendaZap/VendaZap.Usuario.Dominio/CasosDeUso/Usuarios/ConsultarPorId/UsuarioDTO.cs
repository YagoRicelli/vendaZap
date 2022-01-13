using System.Collections.Generic;
using VendaZap.Usuarios.Dominio.CasosDeUso.Chapas.Consultar;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Consultar;
using VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.ConsultarPorId;
using VendaZap.Usuarios.Dominio.Enumeradores;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.ConsultarPorId
{
    public class UsuarioDTO
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public ETipoUsuario Tipo { get; set; }
        public string IdUsuarioLider { get; set; }
        public string IdChapa { get; set; }
        public List<TelefoneDTO> Telefones { get; set; }
        public UsuarioLiderDTO Lider { get; set; }
        public ChapaDTO Chapa { get; set; }
        public bool Ativo { get; set; }
    }
}
