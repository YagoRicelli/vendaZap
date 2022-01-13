namespace VendaZap.Comum.Dominio.Queries
{
    public abstract class QueryHandler<TQuery, TResponse> : DomainBaseItem
          where TQuery : Query
          where TResponse : Response
    {
        public abstract TResponse Handle(TQuery query);
    }

    public interface IQueryHandler<TResponse> where TResponse : Response
    {
        TResponse Handle();
    }
}
