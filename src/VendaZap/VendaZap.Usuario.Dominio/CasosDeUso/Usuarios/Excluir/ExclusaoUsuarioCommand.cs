using VendaZap.Comum.Dominio.Commands;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.Excluir
{
    public class ExclusaoUsuarioCommand : Command
    {
        public ExclusaoUsuarioCommand(string id)
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
