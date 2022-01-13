using VendaZap.Comum.Dominio.Commands;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Excluir
{
    public class ExclusaoTelefoneCommand : Command
    {
        public ExclusaoTelefoneCommand(string id)
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
