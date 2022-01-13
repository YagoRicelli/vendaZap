using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using VendaZap.Usuarios.Dominio.CasosDeUso.ConsultarPorId;

namespace VendaZap.Usuarios.Testes.CasosDeUso.Usuario.ConsultarPorId
{
    public class ConsultaUsuarioPorIdQueryTeste
    {
        [Test]
        public void QuandoQueryPossuirIdInvaliado_RetornarValidacao()
        {
            var query = new ConsultaUsuarioPorIdQuery(string.Empty);

            query.Notifications.Select(x => x.Message).Should().Contain("Identificador é obrigatório.");

            query.Invalid.Should().BeTrue();
        }
    }
}
