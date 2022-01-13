using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Linq;
using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.ValueObjects;
using VendaZap.Comum.Testes.Dominio.Util;
using VendaZap.Usuarios.Dominio.CasosDeUso.RealizarLogin;
using VendaZap.Usuarios.Dominio.Enumeradores;
using VendaZap.Usuarios.Dominio.Repositorios;
using VendaZap.Usuarios.Dominio.Servicos.Auth;

namespace VendaZap.Usuarios.Testes.CasosDeUso.Usuario.RealizarLogin
{
    public class RealizaLoginCommandHandlerTeste
    {
        private RealizaLoginCommand command;
        private RealizaLoginCommandHandler commandHandler;
        private Mock<IUsuarioRepository> repository;
        private Mock<ITokenService> tokenService;

        [SetUp]
        public void Init()
        {
            this.command = new RealizaLoginCommand("yago_@hotmail.com", "123456");

            this.repository = MockHandler.GerarMock<IUsuarioRepository>();
            this.tokenService = MockHandler.GerarMock<ITokenService>();

            this.commandHandler = new RealizaLoginCommandHandler(this.repository.Object, this.tokenService.Object);

            this.ConfiguracarSetups();
        }

        private void ConfiguracarSetups()
        {
            this.tokenService.Setup(x => x.GerarToken(It.IsAny<UsuarioLogado>())).Returns("token");
            this.repository.Setup(x => x.ObterDadosLoginUsuarioPorEmail(It.IsAny<Email>())).Returns(new Login { Id = "1", Senha = new Senha("123456"), Tipo = ETipoUsuario.Administrativo });
        }

        [Test]
        public void QuandoCommandInvalido_RetornaValidacao()
        {
            this.command = new RealizaLoginCommand();

            this.commandHandler.Handle(this.command);

            this.commandHandler.Invalid.Should().BeTrue();
            this.commandHandler.Messages.Should().BeEquivalentTo(this.command.Notifications.Select(x => x.Message));
        }

        [Test]
        public void QuandoUsuarioNaoEncontrado_RetornaValidacao()
        {
            Login login = null;
            this.repository.Setup(x => x.ObterDadosLoginUsuarioPorEmail(It.IsAny<Email>())).Returns(login);

            var ex = Assert.Throws<NotFoundException>(() => this.commandHandler.Handle(this.command));

            ex.Message.Should().Be("Usuário ou senha inválidos.");
        }

        [Test]
        public void QuandoSenhaInformadaForDiferenteCadastrado_RetornaValidacao()
        {
            this.command.Senha = "1234567";

            var ex = Assert.Throws<NotFoundException>(() => this.commandHandler.Handle(this.command));

            ex.Message.Should().Be("Usuário ou senha inválidos.");
        }

        [Test]
        public void QuandoUsuarioEncontrado_RetornarSucesso()
        {
            var response = this.commandHandler.Handle(this.command);

            this.tokenService.Verify(x => x.GerarToken(It.IsAny<UsuarioLogado>()), Times.Once);

            response.Token.Should().NotBeNullOrEmpty();
            response.Id.Should().NotBeNullOrEmpty();
        }
    }
}
