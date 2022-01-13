using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaZap.Usuarios.Dominio.CasosDeUso.RealizarLogin;

namespace VendaZap.Usuarios.Testes.CasosDeUso.Usuario.RealizarLogin
{
    public class RealizaLoginCommandTeste
    {
        [Test]
        public void QuandoComandoPossuirEmailInvaliado_RetornarValidacao()
        {
            var command = new RealizaLoginCommand(string.Empty, "xx");

            command.Notifications.Select(x => x.Message).Should().Contain("O e-mail é obrigatório.");

            command.Invalid.Should().BeTrue();
        }

        [Test]
        public void QuandoComandoPossuirSenhaInvalido_RetornarValidacao()
        {
            var command = new RealizaLoginCommand("yago_@hotmail.com", string.Empty);

            command.Notifications.Select(x => x.Message).Should().Contain("A senha é obrigatória.");

            command.Invalid.Should().BeTrue();
        }
   }
}
