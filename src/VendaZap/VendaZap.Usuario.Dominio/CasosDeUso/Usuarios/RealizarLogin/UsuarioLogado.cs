using VendaZap.Usuarios.Dominio.Enumeradores;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.RealizarLogin
{
    public class UsuarioLogado
    {
        public string Id { get; set; }
        public ETipoUsuario Tipo { get; set; }
    }
}
