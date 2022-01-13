using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.Commands;
using VendaZap.Comum.Dominio.ValueObjects;
using VendaZap.Usuarios.Dominio.Repositorios;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.Editar
{
    public class EdicaoUsuarioCommandHandler : CommandHandler<EdicaoUsuarioCommand, EdicaoUsuarioResponse>
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly IChapaRepository chapaRepository;
        public EdicaoUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IChapaRepository chapaRepository)
        {
            this.usuarioRepository = usuarioRepository;
            this.chapaRepository = chapaRepository;
        }

        public override EdicaoUsuarioResponse Handle(EdicaoUsuarioCommand command)
        {
            if (!command.Validar())
            {
                this.AddNotifications(command.Notifications);
                return null;
            }

            if (!this.chapaRepository.ExisteChapaPorId(command.IdChapa))
            {
                throw new NotFoundException(Mensagem.Chapa.NaoEnconrtrada);
            }

            if (!string.IsNullOrEmpty(command.IdUsuarioLider) && !this.usuarioRepository.ExisteUsuarioLider(command.IdUsuarioLider))
            {
                throw new NotFoundException(Mensagem.CadastrarUsuario.UsuarioLiderNaoEncontrado);
            }

            var usuario = this.usuarioRepository.ObterUsuario(command.Id);
            if (usuario == null)
            {
                throw new NotFoundException(Mensagem.Usuario.NaoEnconrtrado);
            }

            if (usuario.Email.ToString() != command.Email)
            {
                if (this.usuarioRepository.ExisteOutroUsuarioComEmailCadastrado(new Email(command.Email), command.Id))
                {
                    throw new NotFoundException(Mensagem.Usuario.EmailJaCadastradoPorOutroUsuario);
                }
            }

            var usuarioEditado = new UsuarioEdicao(
                usuario.Id,
                command.Nome,
                command.Sobrenome,
                command.Email,
                usuario.Senha,
                command.IdUsuarioLider,
                command.IdChapa,
                command.Tipo);

            this.usuarioRepository.Editar(usuarioEditado);

            return new EdicaoUsuarioResponse(true);
        }
    }
}
