using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.Commands;
using VendaZap.Usuarios.Dominio.Repositorios;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Excluir
{
    public class ExclusaoTelefoneCommandHandler : CommandHandler<ExclusaoTelefoneCommand, ExclusaoTelefoneResponse>
    {
        private readonly ITelefoneRepository telefoneRepository;

        public ExclusaoTelefoneCommandHandler(ITelefoneRepository telefoneRepository)
        {
            this.telefoneRepository = telefoneRepository;
        }

        public override ExclusaoTelefoneResponse Handle(ExclusaoTelefoneCommand command)
        {
            if (!command.Validar())
            {
                this.AddNotifications(command.Notifications);
                return null;
            }

            var telefone = this.telefoneRepository.ObterTelefonePorId(command.Id);
            if (telefone == null)
            {
                throw new NotFoundException(Mensagem.Telefone.NaoEnconrtrado);
            }

            this.telefoneRepository.Excluir(telefone);

            return new ExclusaoTelefoneResponse(true);
        }
    }
}
