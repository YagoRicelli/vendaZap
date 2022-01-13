using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Consultar;

namespace VendaZap.Usuarios.Testes.CasosDeUso.Telefones.Consultar
{
    public class ConsultaTelefoneQueryTeste
    {
        [Test]
        public void QuandoQueryPossuirIdInvaliado_RetornarValidacao()
        {
            var query = new ConsultaTelefoneQuery(string.Empty);

            query.Notifications.Select(x => x.Message).Should().Contain("Identificador usuário é obrigatório.");

            query.Invalid.Should().BeTrue();
        }
    }
}
