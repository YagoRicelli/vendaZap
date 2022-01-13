using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VendaZap.Comum.Dominio.Commands;
using VendaZap.Usuarios.Dominio.CasosDeUso.RealizarLogin;

namespace VendaZap.Api.Publico.Controllers
{
    [AllowAnonymous, Route("api/login")]
    public class LoginController : BaseController
    {
        private CommandHandler<RealizaLoginCommand, RealizaLoginResponse> realizarLoginCommandHandler;

        public LoginController(CommandHandler<RealizaLoginCommand, RealizaLoginResponse> realizarLoginCommandHandler)
        {
            this.realizarLoginCommandHandler = realizarLoginCommandHandler;
        }

        /// <summary>
        ///  Realizar login
        /// </summary>
        /// <param name="command">Dados do usuário</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(RealizaLoginResponse), StatusCodes.Status200OK)]
        public ActionResult<RealizaLoginResponse> RealizarLogin([FromBody] RealizaLoginCommand command)
        {
            var retorno = this.realizarLoginCommandHandler.Handle(command);
            return this.TratarRetorno(retorno, this.realizarLoginCommandHandler);
        }
    }
}
