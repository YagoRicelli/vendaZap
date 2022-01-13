using System;
using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.Enumerador;

namespace VendaZap.Comum.Dominio.Commands
{
    public class ExceptionCommandHandlerDecorator<TCommand, TResponse> : CommandHandler<TCommand, TResponse>
      where TCommand : Command
      where TResponse : Response
    {
        private readonly CommandHandler<TCommand, TResponse> decoratedCommandHandler;

        public ExceptionCommandHandlerDecorator(CommandHandler<TCommand, TResponse> commandHandler)
        {
            this.decoratedCommandHandler = commandHandler;
        }

        public ETipoExcecao TipoExcecao { get; private set; }

        public override TResponse Handle(TCommand command)
        {
            try
            {
                var response = this.decoratedCommandHandler.Handle(command);

                if (this.decoratedCommandHandler.Notifications.Count > 0)
                {
                    this.AddNotifications(this.decoratedCommandHandler.Notifications);
                }

                if (this.decoratedCommandHandler is ExceptionCommandHandlerDecorator<TCommand, TResponse>)
                {
                    this.TipoExcecao = ((ExceptionCommandHandlerDecorator<TCommand, TResponse>)this.decoratedCommandHandler).TipoExcecao;
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
                this.TipoExcecao = ETipoExcecao.BadRequest;
                return default;
            }
        }
    }
}
