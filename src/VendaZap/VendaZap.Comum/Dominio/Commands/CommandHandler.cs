namespace VendaZap.Comum.Dominio.Commands
{
    public abstract class CommandHandler<TCommand, TResponse> : DomainBaseItem
       where TCommand : Command
       where TResponse : Response
    {
        public abstract TResponse Handle(TCommand command);
    }
}
