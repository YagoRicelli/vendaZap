using FluentAssertions;
using NUnit.Framework;
using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.ValueObjects;

namespace VendaZap.Comum.Testes.Dominio.ValueObjects
{
    [TestFixture]
    public class EmailTeste
    {
        [Test]
        public void ObjetoInvalido_QuandoEmailInvalido([Values("a", "a@", "a#ajsidfjis", "a@gmail")] string email)
        {
            var ex = Assert.Throws<BusinessRuleException>(() => { new Email(email); });

            ex.Message.Should().Be("E-mail inválido");
        }

        [Test]
        public void ObjetoValido_QuandoEmailValido([Values("email@email.com", "email_email@email.com")] string email)
        {
            var voEmail = new Email(email);
            voEmail.ToString().Should().Be(email);
        }
    }
}
