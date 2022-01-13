using VendaZap.Comum.Dominio;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.Editar
{
    public class EdicaoUsuarioResponse : Response
    {
        public EdicaoUsuarioResponse(bool sucesso)
        {
            Sucesso = sucesso;
        }

        public bool Sucesso { get; set; }
    }
}
