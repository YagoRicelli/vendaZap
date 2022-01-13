using VendaZap.Comum.Dominio.Queries;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.ConsultarPorId
{
    public class ConsultaUsuarioPorIdQuery : Query
    {
        public ConsultaUsuarioPorIdQuery(string id)
        {
            Id = id;

            this.Validar();
        }

        public string Id { get; set; }
        
        public override bool Validar()
        {
            if (string.IsNullOrEmpty(this.Id))
            {
                this.AddNotification(Mensagem.Usuario.IdObrigatorio);
            }

           return this.Valid;
        }
    }
}
