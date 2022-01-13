using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Linq;
using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.ValueObjects;
using VendaZap.Comum.Testes.Dominio.Util;
using VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.Editar;
using VendaZap.Usuarios.Dominio.Enumeradores;
using VendaZap.Usuarios.Dominio.Repositorios;

namespace VendaZap.Usuarios.Testes.CasosDeUso.Usuario.Editar
{
    public class EdicaoUsuarioCommandHandlerTeste
    {
        private EdicaoUsuarioCommand command;
        private EdicaoUsuarioCommandHandler commandHandler;
        private Mock<IUsuarioRepository> repository;
        private Mock<IChapaRepository> chapaRepository;

        [SetUp]
        public void Init()
        {
            this.command = new EdicaoUsuarioCommand("1", "nome", "sobrenome", "email@email.com", "", "idChapa", ETipoUsuario.Lider);

            this.repository = MockHandler.GerarMock<IUsuarioRepository>();
            this.chapaRepository = MockHandler.GerarMock<IChapaRepository>();

            this.commandHandler = new EdicaoUsuarioCommandHandler(this.repository.Object, this.chapaRepository.Object);

            this.ConfiguracarSetups();
        }

        private void ConfiguracarSetups()
        {
            this.chapaRepository.Setup(x => x.ExisteChapaPorId(It.IsAny<string>())).Returns(true);
            this.repository.Setup(x => x.ExisteUsuarioLider(It.IsAny<string>())).Returns(true);

            var usuario = new Dominio.Entidades.Usuario("yago", "ricelli", "email@hotmail.com", "", new Senha("123456"), "1", ETipoUsuario.Lider);
            this.repository.Setup(x => x.ObterUsuario(It.IsAny<string>())).Returns(usuario);
        }

        [Test]
        public void QuandoCommandInvalido_RetornaValidacao()
        {
            this.command = new EdicaoUsuarioCommand();

            this.commandHandler.Handle(this.command);

            this.commandHandler.Invalid.Should().BeTrue();
            this.commandHandler.Messages.Should().BeEquivalentTo(this.command.Notifications.Select(x => x.Message));
        }

        [Test]
        public void QuandoChapaNaoEncontrada_RetornaValidacao()
        {
            this.chapaRepository.Setup(x => x.ExisteChapaPorId(It.IsAny<string>())).Returns(false);

            var ex = Assert.Throws<NotFoundException>(() => this.commandHandler.Handle(this.command));

            ex.Message.Should().Be("Chapa não encontrada.");
        }

        [Test]
        public void QuandoUsuarioLiderForInformadoENaoExiste_RetornaValidacao()
        {
            this.command.IdUsuarioLider = "1";
            this.repository.Setup(x => x.ExisteUsuarioLider(It.IsAny<string>())).Returns(false);

            var ex = Assert.Throws<NotFoundException>(() => this.commandHandler.Handle(this.command));

            ex.Message.Should().Be("Nenhum usuário lider encontrado.");
        }

        [Test]
        public void QuandoUsuarioEdicaoNaoEncontrado_RetornaValidacao()
        {
            Dominio.Entidades.Usuario usaurio = null;
            this.repository.Setup(x => x.ObterUsuario(It.IsAny<string>())).Returns(usaurio);
            var ex = Assert.Throws<NotFoundException>(() => this.commandHandler.Handle(this.command));

            ex.Message.Should().Be("Usuário não encontrado.");
        }

        [Test]
        public void QuandoUsuarioEdicaoForEncontrado_RealizarEdicao()
        {
            var response = this.commandHandler.Handle(this.command);

            this.repository.Verify(x => x.Editar(It.IsAny<Dominio.Entidades.Usuario>()), Times.Once);
            response.Sucesso.Should().BeTrue();
        }
    }
}
