using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Linq;
using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.ValueObjects;
using VendaZap.Comum.Testes.Dominio.Util;
using VendaZap.Usuarios.Dominio.CasosDeUso.ConsultarPorId;
using VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.ConsultarPorId;
using VendaZap.Usuarios.Dominio.Enumeradores;
using VendaZap.Usuarios.Dominio.Repositorios;

namespace VendaZap.Usuarios.Testes.CasosDeUso.Usuario.ConsultarPorId
{
    public class ConsultaUsuarioPorIdQueryHandlerTeste
    {
        private ConsultaUsuarioPorIdQuery query;
        private ConsultaUsuarioPorIdQueryHandler queryHandler;
        private Mock<IUsuarioRepository> repository;
        private Mock<IChapaRepository> chapaRepository;
        private Mock<ITelefoneRepository> telefoneRepository;

        [SetUp]
        public void Init()
        {
            this.query = new ConsultaUsuarioPorIdQuery("1");

            this.repository = MockHandler.GerarMock<IUsuarioRepository>();
            this.chapaRepository = MockHandler.GerarMock<IChapaRepository>();
            this.telefoneRepository = MockHandler.GerarMock<ITelefoneRepository>();

            this.queryHandler = new ConsultaUsuarioPorIdQueryHandler(this.repository.Object, this.telefoneRepository.Object, this.chapaRepository.Object);

            this.ConfiguracarSetups();
        }

        private void ConfiguracarSetups()
        {
            this.repository.Setup(x => x.ObterUsuarioPorId(It.IsAny<string>())).Returns(new UsuarioDTO { Id ="1", IdUsuarioLider ="2" });
        }

        [Test]
        public void QuandoCommandInvalido_RetornaValidacao()
        {
            this.query = new ConsultaUsuarioPorIdQuery(string.Empty);

            this.queryHandler.Handle(this.query);

            this.queryHandler.Invalid.Should().BeTrue();
            this.queryHandler.Messages.Should().BeEquivalentTo(this.query.Notifications.Select(x => x.Message));
        }

        [Test]
        public void QuandoUsusarioNaoEncontrado_RetornaValidacao()
        {
            UsuarioDTO usuario = null;
            this.repository.Setup(x => x.ObterUsuarioPorId(It.IsAny<string>())).Returns(usuario);

            var ex = Assert.Throws<NotFoundException>(() => this.queryHandler.Handle(this.query));

            ex.Message.Should().Be("Usuário não encontrado.");
        }

        [Test]
        public void QuandoUsuarioPossuirLiderVinculado_RealizarBuscaLider()
        {
            this.repository.Setup(x => x.ObterDadosUsuarioLiderPorId(It.IsAny<string>())).Returns(new UsuarioLiderDTO
            {
                Id = "1",
                Nome = "yago"
            });

            var response = this.queryHandler.Handle(this.query);

            this.repository.Verify(x => x.ObterDadosUsuarioLiderPorId(It.IsAny<string>()), Times.Once);
            response.Lider.Should().NotBeNull();
        }

        [Test]
        public void QuandoUsuarioEncontrado_RealizarBuscaLider()
        {
            var response = this.queryHandler.Handle(this.query);

            response.Id.Should().NotBeNull();
        }
    }
}
