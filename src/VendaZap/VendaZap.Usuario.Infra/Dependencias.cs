using SimpleInjector;
using VendaZap.Comum.Dominio.Commands;
using VendaZap.Comum.Dominio.Queries;
using VendaZap.Usuarios.Dados.Repositorios;
using VendaZap.Usuarios.Dominio.CasosDeUso.Cadastrar;
using VendaZap.Usuarios.Dominio.CasosDeUso.Chapas.Consultar;
using VendaZap.Usuarios.Dominio.CasosDeUso.ConsultarPorId;
using VendaZap.Usuarios.Dominio.CasosDeUso.RealizarLogin;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Cadastrar;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Consultar;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Editar;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Excluir;
using VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.ConsultarLideres;
using VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.Editar;
using VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.Excluir;
using VendaZap.Usuarios.Dominio.Repositorios;
using VendaZap.Usuarios.Dominio.Servicos.Auth;
using VendaZap.Usuarios.Infra.Auth;

namespace VendaZap.Usuarios.Infra
{
    public static class Dependencias
    {
        public static void Resolver(Container container)
        {
            RegistrarHandlers(container);
            RegistrarRepositorios(container);
            RegistrarTokenService(container);
        }

        private static void RegistrarTokenService(Container container)
        {
            container.Register<ITokenService, TokenService>();
        }
        private static void RegistrarRepositorios(Container container)
        {
            container.Register<IUsuarioRepository, UsuarioRepository>();
            container.Register<IChapaRepository, ChapaRepository>();
            container.Register<ITelefoneRepository, TelefoneRepository>();
        }

        private static void RegistrarHandlers(Container container)
        {
            container.Register<CommandHandler<RealizaLoginCommand, RealizaLoginResponse>, RealizaLoginCommandHandler>();
            container.Register<CommandHandler<CadastroUsuarioCommand, CadastroUsuarioResponse>, CadastroUsuarioCommandHandler>();
            container.Register<QueryHandler<ConsultaUsuarioPorIdQuery, ConsultaUsuarioPorIdResponse>, ConsultaUsuarioPorIdQueryHandler>();
            container.Register<CommandHandler<ExclusaoUsuarioCommand, ExclusaoUsuarioResponse>, ExclusaoUsuarioCommandHandler>();
            container.Register<CommandHandler<EdicaoUsuarioCommand, EdicaoUsuarioResponse>, EdicaoUsuarioCommandHandler>();
            container.Register<QueryHandler<ConsultaLideresQuery, ConsultaLideresResponse>, ConsultaLideresQueryHandler>();

            container.Register<QueryHandler<ConsultaTelefoneQuery, ConsultaTelefoneResponse>, ConsultaTelefoneQueryHandler>();
            container.Register<CommandHandler<CadastroTelefoneCommand, CadastroTelefoneResponse>, CadastroTelefoneCommandHandler>();
            container.Register<CommandHandler<EdicaoTelefoneCommand, EdicaoTelefoneResponse>, EdicaoTelefoneCommandHandler>();
            container.Register<CommandHandler<ExclusaoTelefoneCommand, ExclusaoTelefoneResponse>, ExclusaoTelefoneCommandHandler>();

            container.Register<QueryHandler<ConsultaChapaQuery, ConsultaChapaResponse>, ConsultaChapaQueryHandler>();
        }
    }
}
