using VendaZap.Usuarios.Dominio.CasosDeUso.RealizarLogin;

namespace VendaZap.Usuarios.Dominio.Servicos.Auth
{
    public interface ITokenService
    {
        public string GerarToken(UsuarioLogado usuarioLogado);
    }
}
