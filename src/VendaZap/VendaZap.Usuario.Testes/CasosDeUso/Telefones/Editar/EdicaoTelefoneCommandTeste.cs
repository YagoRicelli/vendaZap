using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Editar;

namespace VendaZap.Usuarios.Testes.CasosDeUso.Telefones.Editar
{
    public class EdicaoTelefoneCommandTeste
    {
        [Test]
        public void QuandoComandoPossuirIdInvaliado_RetornarValidacao()
        {
            var command = new EdicaoTelefoneCommand(string.Empty, 31, 35921817);

            command.Notifications.Select(x => x.Message).Should().Contain("Identificador é obrigatório.");
            command.Invalid.Should().BeTrue();
        }

        [Test]
        public void QuandoComandoPossuirDDDInvaliado_RetornarValidacao()
        {
            var command = new EdicaoTelefoneCommand("id", 0, 35921817);

            command.Notifications.Select(x => x.Message).Should().Contain("DDD é obrigatório.");
            command.Invalid.Should().BeTrue();
        }

        [Test]
        public void QuandoComandoPossuirNumeroInvaliado_RetornarValidacao()
        {
            var command = new EdicaoTelefoneCommand("id", 21, 0);

            command.Notifications.Select(x => x.Message).Should().Contain("Número é obrigatório.");
            command.Invalid.Should().BeTrue();
        }
    }
}
