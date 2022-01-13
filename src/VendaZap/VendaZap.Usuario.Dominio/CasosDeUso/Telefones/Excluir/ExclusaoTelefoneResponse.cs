using VendaZap.Comum.Dominio;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Excluir
{
    public class ExclusaoTelefoneResponse : Response
    {
        public ExclusaoTelefoneResponse(bool sucesso)
        {
            Sucesso = sucesso;
        }

        public bool Sucesso { get; set; }
    }
}
