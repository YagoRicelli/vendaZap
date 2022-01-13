using FluentAssertions;
using NUnit.Framework;
using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.ValueObjects;

namespace VendaZap.Comum.Testes.Dominio.ValueObjects
{
    [TestFixture]
    public class SenhaTeste
    {
        [Test]
        public void QuandoSenhaVazia_RetornaValidacao()
        {
            var ex = Assert.Throws<BusinessRuleException>(() => new Senha(""));
            ex.Message.Should().Be("Senha inválida");
        }

        [Test]
        public void QuandoValidarSenhaDiferente_RetornaFalse()
        {
            var senha = new Senha("123456");
            senha.ValidarSenha("654321").Should().BeFalse();
        }

        [Test]
        public void QuandoValidarSenhaIgual_RetornaTrue()
        {
            var senha = new Senha("123456");
            senha.ValidarSenha("123456").Should().BeTrue();
        }

        [Test]
        public void QuandoSenhaValida_RetornaSenhaECriptografa()
        {
            var senha = new Senha("123456");
            senha.ToString().Should().NotBe("123456");
        }
    }
}
