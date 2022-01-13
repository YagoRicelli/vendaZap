using VendaZap.Comum.Dominio;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Editar
{
    public class EdicaoTelefoneResponse : Response
    {
        public EdicaoTelefoneResponse(bool sucesso)
        {
            this.Sucesso = sucesso;
        }

        public bool Sucesso { get; set; }
    }
}
