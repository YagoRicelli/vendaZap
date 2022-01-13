using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Testes.Dominio.Util;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Consultar;
using VendaZap.Usuarios.Dominio.Repositorios;

namespace VendaZap.Usuarios.Testes.CasosDeUso.Telefones.Consultar
{
    public class ConsultaTelefoneQueryHandlerTeste
    {
        private ConsultaTelefoneQuery query;
        private ConsultaTelefoneQueryHandler queryHandler;
        private Mock<ITelefoneRepository> repository;

        [SetUp]
        public void Init()
        {
            this.query = new ConsultaTelefoneQuery("1");

            this.repository = MockHandler.GerarMock<ITelefoneRepository>();

            this.queryHandler = new ConsultaTelefoneQueryHandler(this.repository.Object);

            this.ConfiguracarSetups();
        }

        private void ConfiguracarSetups()
        {
            this.repository.Setup(x => x.ObterTelefonesUsuarioPorIdUsuario(It.IsAny<string>())).Returns(new List<TelefoneDTO>
            {
                new TelefoneDTO{ Id = "1"}
            });
        }

        [Test]
        public void QuandoCommandInvalido_RetornaValidacao()
        {
            this.query = new ConsultaTelefoneQuery(string.Empty);

            this.queryHandler.Handle(this.query);

            this.queryHandler.Invalid.Should().BeTrue();
            this.queryHandler.Messages.Should().BeEquivalentTo(this.query.Notifications.Select(x => x.Message));
        }

        [Test]
        public void QuandoUsuarioNaoPossuirNenhumTelefoneCadastrado_RetornaValidacao()
        {
            var telefones = new List<TelefoneDTO>();

            this.repository.Setup(x => x.ObterTelefonesUsuarioPorIdUsuario(It.IsAny<string>())).Returns(telefones);

            var ex = Assert.Throws<NotFoundException>(() => this.queryHandler.Handle(this.query));

            ex.Message.Should().Be("Usuário não possui nenhum telefone cadastrado.");
        }

        [Test]
        public void QuandoUsuarioPossuirTelefoneCadastrado_RetornarSucesso()
        {
            var response = this.queryHandler.Handle(this.query);

            response.Telefones.Should().NotBeNullOrEmpty();
        }
    }
}
