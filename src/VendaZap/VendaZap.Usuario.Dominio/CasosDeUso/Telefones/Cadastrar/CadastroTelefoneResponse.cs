using VendaZap.Comum.Dominio;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Cadastrar
{
    public class CadastroTelefoneResponse : Response
    {
        public CadastroTelefoneResponse(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
