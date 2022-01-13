using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.Commands;
using VendaZap.Comum.Dominio.ValueObjects;
using VendaZap.Usuarios.Dominio.Entidades;
using VendaZap.Usuarios.Dominio.Repositorios;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Cadastrar
{
    public class CadastroUsuarioCommandHandler : CommandHandler<CadastroUsuarioCommand, CadastroUsuarioResponse>
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IChapaRepository chapaRepository;

        public CadastroUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IChapaRepository chapaRepository)
        {
            this.usuarioRepository = usuarioRepository;
            this.chapaRepository = chapaRepository;
        }

        public override CadastroUsuarioResponse Handle(CadastroUsuarioCommand command)
        {
            if (!command.Validar())
            {
                this.AddNotifications(command.Notifications);
                return null;
            }

            if (!string.IsNullOrEmpty(command.IdUsuarioLider) && this.usuarioRepository.ExisteUsuarioLider(command.IdUsuarioLider))
            {
                throw new NotFoundException(Mensagem.CadastrarUsuario.UsuarioLiderNaoEncontrado);
            }

            if (this.usuarioRepository.JaExisteUsuarioCadastradoPorEmail(new Email(command.Email)))
            {
                throw new NotFoundException(Mensagem.Usuario.EmailJaCadastradoPorOutroUsuario);
            }

            if (!this.chapaRepository.ExisteChapaPorId(command.IdChapa))
            {
                throw new NotFoundException(Mensagem.Chapa.NaoEnconrtrada);
            }

            var novoUsuario = new Usuario(
                 command.Nome,
                 command.Sobrenome,
                 command.Email,
                 command.IdUsuarioLider,
                 new Senha(command.Senha),
                 command.IdChapa,
                 command.Tipo
                );

            this.usuarioRepository.Cadastrar(novoUsuario);
            return new CadastroUsuarioResponse(novoUsuario.Id);
        }
    }
}
