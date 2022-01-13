using VendaZap.Comum.Dominio.Queries;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Consultar
{
    public class ConsultaTelefoneQuery : Query
    {
        public ConsultaTelefoneQuery(string idUsuario)
        {
            IdUsuario = idUsuario;

            this.Validar();
        }

        public string IdUsuario { get; set; }

        public override bool Validar()
        {
            if (string.IsNullOrEmpty(this.IdUsuario))
            {
                this.AddNotification(Mensagem.Telefone.IdUsuarioObrigatorio);
            }

            return this.Valid;
        }
    }
}
