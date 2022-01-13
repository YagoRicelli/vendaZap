using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Linq;
using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Testes.Dominio.Util;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Excluir;
using VendaZap.Usuarios.Dominio.Repositorios;

namespace VendaZap.Usuarios.Testes.CasosDeUso.Telefones.Excluir
{
    public class ExclusaoTelefoneCommandHandlerTeste
    {
        private ExclusaoTelefoneCommand command;
        private ExclusaoTelefoneCommandHandler commandHandler;
        private Mock<ITelefoneRepository> repository;

        [SetUp]
        public void Init()
        {
            this.command = new ExclusaoTelefoneCommand("id");

            this.repository = MockHandler.GerarMock<ITelefoneRepository>();
        
            this.commandHandler = new ExclusaoTelefoneCommandHandler(this.repository.Object);

            this.ConfiguracarSetups();
        }

        private void ConfiguracarSetups()
        {
            var telefone = new Dominio.Entidades.Telefone(31,35921817,"idUsuario");
            this.repository.Setup(x => x.ObterTelefonePorId(It.IsAny<string>())).Returns(telefone);
        }

        [Test]
        public void QuandoCommandInvalido_RetornaValidacao()
        {
            this.command = new ExclusaoTelefoneCommand(string.Empty);

            this.commandHandler.Handle(this.command);

            this.commandHandler.Invalid.Should().BeTrue();
            this.commandHandler.Messages.Should().BeEquivalentTo(this.command.Notifications.Select(x => x.Message));
        }

        [Test]
        public void QuandoTelefoneInformadoNaoEncontrado_RetornaValidacao()
        {
            Dominio.Entidades.Telefone telefone = null;
            this.repository.Setup(x => x.ObterTelefonePorId(It.IsAny<string>())).Returns(telefone);

            var ex = Assert.Throws<NotFoundException>(() => this.commandHandler.Handle(this.command));

            ex.Message.Should().Be("Telefone não encontrado.");
        }

        [Test]
        public void QuandoTelefoneEncontrado_RealizarExclusao()
        {
            var response = this.commandHandler.Handle(this.command);

            this.repository.Verify(x => x.Excluir(It.IsAny<Dominio.Entidades.Telefone>()), Times.Once);
            response.Sucesso.Should().BeTrue();
        }
    }
}
