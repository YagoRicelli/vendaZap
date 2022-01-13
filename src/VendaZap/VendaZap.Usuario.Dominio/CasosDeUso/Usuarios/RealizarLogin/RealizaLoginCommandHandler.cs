
using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.Commands;
using VendaZap.Comum.Dominio.ValueObjects;
using VendaZap.Usuarios.Dominio.Repositorios;
using VendaZap.Usuarios.Dominio.Servicos.Auth;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.RealizarLogin
{
    public class RealizaLoginCommandHandler : CommandHandler<RealizaLoginCommand, RealizaLoginResponse>
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ITokenService tokenService;

        public RealizaLoginCommandHandler(IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            this.usuarioRepository = usuarioRepository;
            this.tokenService = tokenService;
        }

        public override RealizaLoginResponse Handle(RealizaLoginCommand command)
        {
            if (!command.Validar())
            {
                this.AddNotifications(command.Notifications);
                return null;
            }

            var email = new Email(command.Email);

            var login = this.usuarioRepository.ObterDadosLoginUsuarioPorEmail(email);
            if (login == null || !login.Senha.ValidarSenha(command.Senha))
            {
                throw new NotFoundException(Mensagem.RealizarLogin.UsuarioOuSenhaInvalidos);
            }

            var tokenGerado = this.tokenService.GerarToken(new UsuarioLogado { Id = login.Id, Tipo = login.Tipo });

            return new RealizaLoginResponse(login.Id, tokenGerado);
        }
    }
}
