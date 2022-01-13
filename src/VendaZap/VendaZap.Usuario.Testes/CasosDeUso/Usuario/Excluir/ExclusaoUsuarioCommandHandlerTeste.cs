using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.ValueObjects;
using VendaZap.Comum.Testes.Dominio.Util;
using VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.Excluir;
using VendaZap.Usuarios.Dominio.Enumeradores;
using VendaZap.Usuarios.Dominio.Repositorios;

namespace VendaZap.Usuarios.Testes.CasosDeUso.Usuario.Excluir
{
    public class ExclusaoUsuarioCommandHandlerTeste
    {
        private ExclusaoUsuarioCommand command;
        private ExclusaoUsuarioCommandHandler commandHandler;
        private Mock<IUsuarioRepository> repository;

        [SetUp]
        public void Init()
        {
            this.command = new ExclusaoUsuarioCommand("1");

            this.repository = MockHandler.GerarMock<IUsuarioRepository>();

            this.commandHandler = new ExclusaoUsuarioCommandHandler(this.repository.Object);

            this.ConfiguracarSetups();
        }

        private void ConfiguracarSetups()
        {
            var usuario = new Dominio.Entidades.Usuario("yago", "ricelli", "email@hotmail.com", "", new Senha("123456"), "1", ETipoUsuario.Lider);

            this.repository.Setup(x => x.ObterUsuario(It.IsAny<string>())).Returns(usuario);
        }

        [Test]
        public void QuandoCommandInvalido_RetornaValidacao()
        {
            this.command = new ExclusaoUsuarioCommand(string.Empty);

            this.commandHandler.Handle(this.command);

            this.commandHandler.Invalid.Should().BeTrue();
            this.commandHandler.Messages.Should().BeEquivalentTo(this.command.Notifications.Select(x => x.Message));
        }

        [Test]
        public void QuandoUsuarioNaoEncontrado_RetornaValidacao()
        {
            Dominio.Entidades.Usuario usuario = null;
            this.repository.Setup(x => x.ObterUsuario(It.IsAny<string>())).Returns(usuario);

            var ex = Assert.Throws<NotFoundException>(() => this.commandHandler.Handle(this.command));

            ex.Message.Should().Be("Usuário não encontrado.");
        }

        [Test]
        public void QuandoUsuarioEncontrado_InativarUsuario()
        {
            var response = this.commandHandler.Handle(this.command);

            this.repository.Verify(x => x.Editar(It.Is<Dominio.Entidades.Usuario>(x => string.IsNullOrEmpty(x.IdUsuarioLider))), Times.Once);
            response.Sucesso.Should().BeTrue();
        }

        [Test]
        public void QuandoUsuarioForTipoLider_RealizarConsultaUsuriosVinculado()
        {
            var usuario = new Dominio.Entidades.Usuario("yago", "ricelli", "email@hotmail.com", "", new Senha("123456"), "1", ETipoUsuario.Lider);
            this.repository.Setup(x => x.ObterUsuario(It.IsAny<string>())).Returns(usuario);

            var response = this.commandHandler.Handle(this.command);

            this.repository.Verify(x => x.ObterUsuariosVinculadosAoLiderPorIdLider(It.IsAny<string>()), Times.Once);

            response.Sucesso.Should().BeTrue();
        }

        [Test]
        public void QuandoUsuarioForTipoLiderEPossuiVinculo_RealizarDesvinculo()
        {
            var usuario = new Dominio.Entidades.Usuario("yago", "ricelli", "email@hotmail.com", "", new Senha("123456"), "1", ETipoUsuario.Lider);
            this.repository.Setup(x => x.ObterUsuario(It.IsAny<string>())).Returns(usuario);

            var usaurios = new List<Dominio.Entidades.Usuario>() { new Dominio.Entidades.Usuario("teste", "teste", "email@hotmail.com", "", new Senha("123456"), "1", ETipoUsuario.Lider) };
            this.repository.Setup(x => x.ObterUsuariosVinculadosAoLiderPorIdLider(It.IsAny<string>())).Returns(usaurios);

            var response = this.commandHandler.Handle(this.command);

            this.repository.Verify(x => x.Editar(It.IsAny<List<Dominio.Entidades.Usuario>>()), Times.Once);
            this.repository.Verify(x => x.ObterUsuariosVinculadosAoLiderPorIdLider(It.IsAny<string>()), Times.Once);

            response.Sucesso.Should().BeTrue();
        }
    }
}
