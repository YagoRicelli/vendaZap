using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Excluir;

namespace VendaZap.Usuarios.Testes.CasosDeUso.Telefones.Excluir
{
    public class ExclusaoTelefoneCommandTeste
    {
        [Test]
        public void QuandoComandoPossuirIdInvaliado_RetornarValidacao()
        {
            var command = new ExclusaoTelefoneCommand(string.Empty);

            command.Notifications.Select(x => x.Message).Should().Contain("Identificador é obrigatório.");
            command.Invalid.Should().BeTrue();
        }
    }
}
