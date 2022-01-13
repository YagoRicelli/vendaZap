using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using VendaZap.Usuarios.Dominio.CasosDeUso.Cadastrar;
using VendaZap.Usuarios.Dominio.Enumeradores;

namespace VendaZap.Usuarios.Testes.CasosDeUso.Usuario.Cadastrar
{
    public class CadastroUsuarioCommandTeste
    {
        [Test]
        public void QuandoComandoPossuirNomeInvaliado_RetornarValidacao()
        {
            var command = new CadastroUsuarioCommand(string.Empty, "sobrenome", "email@email.com", "senha", "idUsuarioLider", "idChapa", ETipoUsuario.Lider);

            command.Notifications.Select(x => x.Message).Should().Contain("Nome do usuário é obrigatório.");
            command.Invalid.Should().BeTrue();
        }

        [Test]
        public void QuandoComandoPossuirSobrenomeInvaliado_RetornarValidacao()
        {
            var command = new CadastroUsuarioCommand("nome", string.Empty, "email@email.com", "senha", "idUsuarioLider", "idChapa", ETipoUsuario.Lider);

            command.Notifications.Select(x => x.Message).Should().Contain("Sobrenome do usuário é obrigatório.");
            command.Invalid.Should().BeTrue();
        }

        [Test]
        public void QuandoComandoPossuirEmailInvaliado_RetornarValidacao()
        {
            var command = new CadastroUsuarioCommand("nome","sobrenome", "", "senha", "idUsuarioLider", "idChapa", ETipoUsuario.Lider);

            command.Notifications.Select(x => x.Message).Should().Contain("O e-mail do usuário é obrigatório.");
            command.Invalid.Should().BeTrue();
        }

        [Test]
        public void QuandoComandoPossuirSenhaInvaliado_RetornarValidacao()
        {
            var command = new CadastroUsuarioCommand("nome", "sobrenome", "email@email.com", "", "idUsuarioLider", "idChapa", ETipoUsuario.Lider);

            command.Notifications.Select(x => x.Message).Should().Contain("Senha é obrigatória.");
            command.Invalid.Should().BeTrue();
        }

        [Test]
        public void QuandoComandoPossuirIdChapaInvaliado_RetornarValidacao()
        {
            var command = new CadastroUsuarioCommand("nome", "sobrenome", "email@email.com", "senha", "idUsuarioLider", "", ETipoUsuario.Lider);

            command.Notifications.Select(x => x.Message).Should().Contain("Identificador é obrigatório.");
            command.Invalid.Should().BeTrue();
        }
    }
}
