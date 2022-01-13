using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.Editar;
using VendaZap.Usuarios.Dominio.Enumeradores;

namespace VendaZap.Usuarios.Testes.CasosDeUso.Usuario.Editar
{
    public class EdicaoUsuarioCommandTeste
    {
        [Test]
        public void QuandoComandoPossuirIdInvaliado_RetornarValidacao()
        {
            var command = new EdicaoUsuarioCommand(string.Empty,"nome","sobrenome","email@email.com","","idChapa", ETipoUsuario.Lider);

            command.Notifications.Select(x => x.Message).Should().Contain("Identificador é obrigatório.");

            command.Invalid.Should().BeTrue();
        }

        [Test]
        public void QuandoComandoPossuirNomeInvaliado_RetornarValidacao()
        {
            var command = new EdicaoUsuarioCommand("id",string.Empty, "sobrenome", "email@email.com", "", "idChapa", ETipoUsuario.Lider);

            command.Notifications.Select(x => x.Message).Should().Contain("Nome do usuário é obrigatório.");

            command.Invalid.Should().BeTrue();
        }

        [Test]
        public void QuandoComandoPossuirSobrenomeInvaliado_RetornarValidacao()
        {
            var command = new EdicaoUsuarioCommand("id", "nome", string.Empty, "email@email.com", "", "idChapa", ETipoUsuario.Lider);

            command.Notifications.Select(x => x.Message).Should().Contain("Sobrenome do usuário é obrigatório.");

            command.Invalid.Should().BeTrue();
        }

        [Test]
        public void QuandoComandoPossuirEmailInvaliado_RetornarValidacao()
        {
            var command = new EdicaoUsuarioCommand("id", "nome", "sobrenome", string.Empty, "", "idChapa", ETipoUsuario.Lider);

            command.Notifications.Select(x => x.Message).Should().Contain("O e-mail do usuário é obrigatório.");

            command.Invalid.Should().BeTrue();
        }

        [Test]
        public void QuandoComandoPossuirIdChapaInvaliado_RetornarValidacao()
        {
            var command = new EdicaoUsuarioCommand("id", "nome", "sobrenome", "email@email.com", "", string.Empty, ETipoUsuario.Lider);

            command.Notifications.Select(x => x.Message).Should().Contain("Identificador é obrigatório.");

            command.Invalid.Should().BeTrue();
        }
    }
}
