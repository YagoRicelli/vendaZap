using VendaZap.Comum.Dominio;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.Excluir
{
    public class ExclusaoUsuarioResponse : Response
    {
        public ExclusaoUsuarioResponse(bool sucesso)
        {
            Sucesso = sucesso;
        }

        public bool Sucesso { get; set; }
    }
}
