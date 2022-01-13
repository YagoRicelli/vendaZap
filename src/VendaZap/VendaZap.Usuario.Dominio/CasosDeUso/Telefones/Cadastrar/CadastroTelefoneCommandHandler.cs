using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.Commands;
using VendaZap.Usuarios.Dominio.Entidades;
using VendaZap.Usuarios.Dominio.Repositorios;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Cadastrar
{
    public class CadastroTelefoneCommandHandler : CommandHandler<CadastroTelefoneCommand, CadastroTelefoneResponse>
    {
        public readonly ITelefoneRepository telefoneRepository;
        private readonly IUsuarioRepository usuarioRepository;
        public CadastroTelefoneCommandHandler(ITelefoneRepository telefoneRepository, IUsuarioRepository usuarioRepository)
        {
            this.telefoneRepository = telefoneRepository;
            this.usuarioRepository = usuarioRepository;
        }

        public override CadastroTelefoneResponse Handle(CadastroTelefoneCommand command)
        {
            if (!command.Validar())
            {
                this.AddNotifications(command.Notifications);
                return null;
            }

            if (!this.usuarioRepository.ExistePorId(command.IdUsuario))
            {
                throw new NotFoundException(Mensagem.Usuario.NaoEnconrtrado);
            }

            var novoTelefone = new Telefone(command.DDD,command.Numero,command.IdUsuario);

            this.telefoneRepository.Cadastrar(novoTelefone);
             
            return new CadastroTelefoneResponse(novoTelefone.Id);
        }
    }
}
