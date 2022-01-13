using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Linq;
using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Testes.Dominio.Util;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Cadastrar;
using VendaZap.Usuarios.Dominio.Repositorios;

namespace VendaZap.Usuarios.Testes.CasosDeUso.Telefones.Cadastrar
{
    public class CadastroTelefoneCommandHandlerTeste
    {
        private CadastroTelefoneCommand command;
        private CadastroTelefoneCommandHandler commandHandler;
        private Mock<ITelefoneRepository> repository;
        private Mock<IUsuarioRepository> usarioRepository;

        [SetUp]
        public void Init()
        {
            this.command = new CadastroTelefoneCommand("id", 31, 35921817);

            this.repository = MockHandler.GerarMock<ITelefoneRepository>();
            this.usarioRepository = MockHandler.GerarMock<IUsuarioRepository>();

            this.commandHandler = new CadastroTelefoneCommandHandler(this.repository.Object, this.usarioRepository.Object);

            this.ConfiguracarSetups();
        }

        private void ConfiguracarSetups()
        {
            this.usarioRepository.Setup(x => x.ExistePorId(It.IsAny<string>())).Returns(true);
        }

        [Test]
        public void QuandoCommandInvalido_RetornaValidacao()
        {
            this.command = new CadastroTelefoneCommand();

            this.commandHandler.Handle(this.command);

            this.commandHandler.Invalid.Should().BeTrue();
            this.commandHandler.Messages.Should().BeEquivalentTo(this.command.Notifications.Select(x => x.Message));
        }

        [Test]
        public void QuandoNaoExistirUsuarioPeloIdInformado_RetornaValidacao()
        {
            this.usarioRepository.Setup(x => x.ExistePorId(It.IsAny<string>())).Returns(false);

            var ex = Assert.Throws<NotFoundException>(() => this.commandHandler.Handle(this.command));

            ex.Message.Should().Be("Usuário não encontrado.");
        }

        [Test]
        public void QuandoExistirUsuario_RegistrarNovoTelefone()
        {
            var response = this.commandHandler.Handle(this.command);

            this.repository.Verify(x => x.Cadastrar(It.IsAny<Dominio.Entidades.Telefone>()), Times.Once);
            response.Id.Should().NotBeNullOrEmpty();
        }
    }
}
