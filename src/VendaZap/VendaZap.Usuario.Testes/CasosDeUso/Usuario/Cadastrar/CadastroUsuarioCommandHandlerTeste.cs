using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Linq;
using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.ValueObjects;
using VendaZap.Comum.Testes.Dominio.Util;
using VendaZap.Usuarios.Dominio.CasosDeUso.Cadastrar;
using VendaZap.Usuarios.Dominio.Enumeradores;
using VendaZap.Usuarios.Dominio.Repositorios;

namespace VendaZap.Usuarios.Testes.CasosDeUso.Usuario.Cadastrar
{
    public class CadastroUsuarioCommandHandlerTeste
    {
        private CadastroUsuarioCommand command;
        private CadastroUsuarioCommandHandler commandHandler;
        private Mock<IUsuarioRepository> repository;
        private Mock<IChapaRepository> chapaRepository;

        [SetUp]
        public void Init()
        {
            this.command = new CadastroUsuarioCommand("nome", "sobrenome", "yago@hotmail.com", "123456", "", "idChapa", ETipoUsuario.Lider);

            this.repository = MockHandler.GerarMock<IUsuarioRepository>();
            this.chapaRepository = MockHandler.GerarMock<IChapaRepository>();

            this.commandHandler = new CadastroUsuarioCommandHandler(this.repository.Object, this.chapaRepository.Object);

            this.ConfiguracarSetups();
        }

        private void ConfiguracarSetups()
        {
            this.repository.Setup(x => x.ExisteUsuarioLider(It.IsAny<string>())).Returns(false);
            this.chapaRepository.Setup(x => x.ExisteChapaPorId(It.IsAny<string>())).Returns(true);
            this.repository.Setup(x => x.ExisteUsuarioLider(It.IsAny<string>())).Returns(false);


            var usuario = new Dominio.Entidades.Usuario("yago", "ricelli", "yago@hotmail.com", "", new Senha("123456"), "1", ETipoUsuario.Lider);
            this.repository.Setup(x => x.ObterUsuario(It.IsAny<string>())).Returns(usuario);
        }

        [Test]
        public void QuandoCommandInvalido_RetornaValidacao()
        {
            this.command = new CadastroUsuarioCommand();

            this.commandHandler.Handle(this.command);

            this.commandHandler.Invalid.Should().BeTrue();
            this.commandHandler.Messages.Should().BeEquivalentTo(this.command.Notifications.Select(x => x.Message));
        }

        [Test]
        public void QuandoUsuarioLiderForInformadoENaoExiste_RetornaValidacao()
        {
            this.command.IdUsuarioLider = "1";
            this.repository.Setup(x => x.ExisteUsuarioLider(It.IsAny<string>())).Returns(true);

            var ex = Assert.Throws<NotFoundException>(() => this.commandHandler.Handle(this.command));

            ex.Message.Should().Be("Nenhum usuário lider encontrado.");
        }

        [Test]
        public void QuandoJaExisteUsuarioCadastradoComEmailInformado_RetornaValidacao()
        {
            this.repository.Setup(x => x.JaExisteUsuarioCadastradoPorEmail(It.IsAny<Email>())).Returns(true);

            var ex = Assert.Throws<NotFoundException>(() => this.commandHandler.Handle(this.command));

            ex.Message.Should().Be("O e-mail informado já está sendo utilizado por outro usuário.");
        }

        [Test]
        public void QuandoCommandPossuiIdChapaInvalido_RetornaValidacao()
        {
            this.chapaRepository.Setup(x => x.ExisteChapaPorId(It.IsAny<string>())).Returns(false);

            var ex = Assert.Throws<NotFoundException>(() => this.commandHandler.Handle(this.command));

            ex.Message.Should().Be("Chapa não encontrada.");
        }

        [Test]
        public void QuandoCommandPossuirDadosValidos_RetornarSucesso()
        {
            var response = this.commandHandler.Handle(this.command);

            this.repository.Verify(x => x.Cadastrar(It.IsAny<Dominio.Entidades.Usuario>()), Times.Once);
            response.Id.Should().NotBeNullOrEmpty();
        }
    }
}
