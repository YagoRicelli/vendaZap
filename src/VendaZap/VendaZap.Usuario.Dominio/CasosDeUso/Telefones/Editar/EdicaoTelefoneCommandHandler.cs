using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.Commands;
using VendaZap.Usuarios.Dominio.Repositorios;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Editar
{
    public class EdicaoTelefoneCommandHandler : CommandHandler<EdicaoTelefoneCommand, EdicaoTelefoneResponse>
    {
        public readonly ITelefoneRepository telefoneRepository;

        public EdicaoTelefoneCommandHandler(ITelefoneRepository telefoneRepository)
        {
            this.telefoneRepository = telefoneRepository;
        }

        public override EdicaoTelefoneResponse Handle(EdicaoTelefoneCommand command)
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

            var telefoneEditado = new TelefoneEdicao(
                telefone.Id,
                command.DDD,
                command.Numero,
                telefone.IdUsuario);

            this.telefoneRepository.Editar(telefoneEditado);

            return new EdicaoTelefoneResponse(true);
        }
    }
}
