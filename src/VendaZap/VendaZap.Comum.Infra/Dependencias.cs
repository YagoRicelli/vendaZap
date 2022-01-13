using SimpleInjector;
using VendaZap.Comum.Dominio.Commands;
using VendaZap.Comum.Dominio.Queries;

namespace VendaZap.Comum.Infra
{
    public static class Dependencias
    {
        public static void Resolver(Container container)
        {
            RegistrarDecorators(container);
        }

        private static void RegistrarDecorators(Container container)
        {
            container.RegisterDecorator(typeof(CommandHandler<,>), typeof(ExceptionCommandHandlerDecorator<,>));
            container.RegisterDecorator(typeof(QueryHandler<,>), typeof(ExceptionQueryHandlerDecorator<,>));
        }
    }
}
