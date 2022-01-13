using System.Linq;
using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.Commands;
using VendaZap.Usuarios.Dominio.Entidades;
using VendaZap.Usuarios.Dominio.Repositorios;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.Excluir
{
    public class ExclusaoUsuarioCommandHandler : CommandHandler<ExclusaoUsuarioCommand, ExclusaoUsuarioResponse>
    {
        private readonly IUsuarioRepository usuarioRepository;

        public ExclusaoUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public override ExclusaoUsuarioResponse Handle(ExclusaoUsuarioCommand command)
        {
            if (!command.Validar())
            {
                this.AddNotifications(command.Notifications);
                return null;
            }

            var usuario = this.usuarioRepository.ObterUsuario(command.Id);
            if (usuario == null)
            {
                throw new NotFoundException(Mensagem.Usuario.NaoEnconrtrado);
            }

            if (usuario.Tipo == Enumeradores.ETipoUsuario.Lider)
            {
                this.RemoverVinculoLider(usuario.Id);
            }

            usuario.Inativar();

            this.usuarioRepository.Editar(usuario);

            return new ExclusaoUsuarioResponse(true);
        }

        private void RemoverVinculoLider(string idLider)
        {
            var usuariosVinculado = this.usuarioRepository.ObterUsuariosVinculadosAoLiderPorIdLider(idLider);
            if (usuariosVinculado == null || !usuariosVinculado.Any())
            {
                return;
            }

            usuariosVinculado.ForEach(x => x.RemoverLider());

            this.usuarioRepository.Editar(usuariosVinculado);
        }
    }
}
