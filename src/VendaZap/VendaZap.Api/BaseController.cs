using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using VendaZap.Comum.Dominio;
using VendaZap.Comum.Dominio.Commands;
using VendaZap.Comum.Dominio.Enumerador;
using VendaZap.Comum.Dominio.Messages;
using VendaZap.Comum.Dominio.Queries;

namespace VendaZap.Api
{
    public class BaseController : ControllerBase
    {
        protected ActionResult TratarRetorno<TCommand, TResponse>(TResponse retorno, CommandHandler<TCommand, TResponse> commandHandler)
            where TCommand : Command
            where TResponse : Response
        {
            var decoratedCommandHandler = commandHandler as ExceptionCommandHandlerDecorator<TCommand, TResponse>;
            if (decoratedCommandHandler != null)
            {
                return this.TratarRetorno(retorno, decoratedCommandHandler.TipoExcecao, commandHandler.Notifications);
            }

            return Ok(retorno);
        }

        protected ActionResult TratarRetorno<TQuery, TResponse>(TResponse retorno, QueryHandler<TQuery, TResponse> queryHandler)
            where TQuery : Query
            where TResponse : Response
        {
            var decoratedCommandHandler = queryHandler as ExceptionQueryHandlerDecorator<TQuery, TResponse>;
            if (decoratedCommandHandler != null)
            {
                return this.TratarRetorno(retorno, decoratedCommandHandler.TipoExcecao, queryHandler.Notifications);
            }

            return Ok(retorno);
        }


        private ActionResult TratarRetorno<TResponse>(TResponse retorno, ETipoExcecao tipoExcecao, List<Notification> notificacoes)
            where TResponse : Response
        {
            switch (tipoExcecao)
            {
                case ETipoExcecao.BadRequest:
                    return BadRequest(notificacoes);
                case ETipoExcecao.NotFound:
                    return NotFound(notificacoes);
                case ETipoExcecao.Conflict:
                    return Conflict(notificacoes);
                case ETipoExcecao.Nenhum:
                default:
                    break;
            }

            if (notificacoes != null && notificacoes.Any())
            {
                return BadRequest(notificacoes);
            }

            if (retorno == null)
            {
                return NoContent();
            }

            return Ok(retorno);
        }
    }
}
