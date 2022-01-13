using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Cadastrar;

namespace VendaZap.Usuarios.Testes.CasosDeUso.Telefones.Cadastrar
{
    public class CadastroTelefoneCommandTeste
    {
        [Test]
        public void QuandoQueryPossuirIdUsuarioInvaliado_RetornarValidacao()
        {
            var query = new CadastroTelefoneCommand(string.Empty, 31, 35921817);

            query.Notifications.Select(x => x.Message).Should().Contain("Identificador é obrigatório.");

            query.Invalid.Should().BeTrue();
        }

        [Test]
        public void QuandoComandoPossuirDDDInvaliado_RetornarValidacao()
        {
            var command = new CadastroTelefoneCommand("id", 0, 35921817);

            command.Notifications.Select(x => x.Message).Should().Contain("DDD é obrigatório.");
            command.Invalid.Should().BeTrue();
        }

        [Test]
        public void QuandoComandoPossuirNumeroInvaliado_RetornarValidacao()
        {
            var command = new CadastroTelefoneCommand("id", 21, 0);

            command.Notifications.Select(x => x.Message).Should().Contain("Número é obrigatório.");
            command.Invalid.Should().BeTrue();
        }
    }
}
