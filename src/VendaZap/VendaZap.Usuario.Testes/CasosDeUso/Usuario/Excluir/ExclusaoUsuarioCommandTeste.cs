using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.Excluir;

namespace VendaZap.Usuarios.Testes.CasosDeUso.Usuario.Excluir
{
    public class ExclusaoUsuarioCommandTeste
    {
        [Test]
        public void QuandoComandoPossuirIdInvaliado_RetornarValidacao()
        {
            var command = new ExclusaoUsuarioCommand(string.Empty);

            command.Notifications.Select(x => x.Message).Should().Contain("Identificador é obrigatório.");

            command.Invalid.Should().BeTrue();
        }

        [Test]
        public void QuandoComandoPossuirIdInformado_RetornarSucesso()
        {
            var command = new ExclusaoUsuarioCommand("id");

            command.Invalid.Should().BeFalse();
        }
    }
}
