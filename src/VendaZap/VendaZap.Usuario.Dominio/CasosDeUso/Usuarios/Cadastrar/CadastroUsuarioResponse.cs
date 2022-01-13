using VendaZap.Comum.Dominio;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Cadastrar
{
    public class CadastroUsuarioResponse : Response
    {
        public CadastroUsuarioResponse(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
