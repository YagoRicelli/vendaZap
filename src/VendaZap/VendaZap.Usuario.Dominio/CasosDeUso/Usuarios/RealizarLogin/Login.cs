using VendaZap.Comum.Dominio.ValueObjects;
using VendaZap.Usuarios.Dominio.Enumeradores;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.RealizarLogin
{
    public class Login
    {
        public string Id { get; set; }
        public Senha Senha { get; set; }
        public ETipoUsuario Tipo { get; set; }
    }
}
