using System;
using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.Enumerador;

namespace VendaZap.Comum.Dominio.Queries
{
    public class ExceptionQueryHandlerDecorator<TQuery, TResponse> : QueryHandler<TQuery, TResponse>
      where TQuery : Query
      where TResponse : Response
    {
        public ETipoExcecao TipoExcecao { get; private set; }

        private readonly QueryHandler<TQuery, TResponse> decoratedQueryHandler;

        public ExceptionQueryHandlerDecorator(QueryHandler<TQuery, TResponse> queryHandler)
        {
            this.decoratedQueryHandler = queryHandler;
            this.TipoExcecao = ETipoExcecao.Nenhum;
        }

        public override TResponse Handle(TQuery query)
        {
            try
            {
                var response = this.decoratedQueryHandler.Handle(query);

                if (this.decoratedQueryHandler.Notifications.Count > 0)
                {
                    this.AddNotifications(this.decoratedQueryHandler.Notifications);
                }

                if (this.decoratedQueryHandler is ExceptionQueryHandlerDecorator<TQuery, TResponse>)
                {
                    this.TipoExcecao = ((ExceptionQueryHandlerDecorator<TQuery, TResponse>)this.decoratedQueryHandler).TipoExcecao;
                }

                return response;
            }
            catch (ConflictException ex)
            {
                this.TipoExcecao = ETipoExcecao.Conflict;
                this.AddNotification(ex.Message);
                return default;
            }
            catch (NotFoundException ex)
            {
                this.TipoExcecao = ETipoExcecao.NotFound;
                this.AddNotification(ex.Message);
                return default;
            }
            catch (BusinessRuleException ex)
            {
                this.TipoExcecao = ETipoExcecao.BadRequest;
                this.AddNotification(ex.Message);
                return default;
            }
            catch (Exception ex)
            {
                return default;
            }
        }
    }
}
