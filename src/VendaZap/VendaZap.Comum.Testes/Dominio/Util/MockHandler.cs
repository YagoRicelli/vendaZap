using Moq;
using System.Linq;

namespace VendaZap.Comum.Testes.Dominio.Util
{
    public class MockHandler
    {
        public static Mock<TMock> GerarMock<TMock>() where TMock : class
        {
            var dadosDoTipo = typeof(TMock);

            if (dadosDoTipo.IsInterface || dadosDoTipo.IsAbstract || dadosDoTipo.GetConstructors().Any(x => x.GetParameters().Length == 0))
            {
                return new Mock<TMock>();
            }

            var maiorConstrutor = dadosDoTipo.GetConstructors().OrderByDescending(x => x.GetParameters()).First();

            var qtdParametros = maiorConstrutor.GetParameters().Length;

            var mock = new Mock<TMock>(Enumerable.Range(0, qtdParametros).Select(x => (object)null).ToArray());

            return mock;
        }
    }
}
