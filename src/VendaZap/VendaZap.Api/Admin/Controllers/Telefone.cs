using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VendaZap.Comum.Dominio.Commands;
using VendaZap.Comum.Dominio.Queries;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Cadastrar;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Consultar;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Editar;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Excluir;

namespace VendaZap.Api.Admin.Controllers
{
    [AllowAnonymous, Route("api/usuarios/{idUsuario}/telefones")]
    public class Telefone : BaseController
    {
        private QueryHandler<ConsultaTelefoneQuery, ConsultaTelefoneResponse> consultaTelefoneQueryHandler;
        private CommandHandler<CadastroTelefoneCommand, CadastroTelefoneResponse> cadastroTelefoneCommandHandler;
        private CommandHandler<EdicaoTelefoneCommand, EdicaoTelefoneResponse> edicaoTelefoneCommandHandler;
        private CommandHandler<ExclusaoTelefoneCommand, ExclusaoTelefoneResponse> exclusaoTelefoneCommandHandler;

        public Telefone(QueryHandler<ConsultaTelefoneQuery, ConsultaTelefoneResponse> consultaTelefoneQueryHandler, CommandHandler<CadastroTelefoneCommand, CadastroTelefoneResponse> cadastroTelefoneCommandHandler, CommandHandler<EdicaoTelefoneCommand, EdicaoTelefoneResponse> edicaoTelefoneCommandHandler, CommandHandler<ExclusaoTelefoneCommand, ExclusaoTelefoneResponse> exclusaoTelefoneCommandHandler)
        {
            this.consultaTelefoneQueryHandler = consultaTelefoneQueryHandler;
            this.cadastroTelefoneCommandHandler = cadastroTelefoneCommandHandler;
            this.edicaoTelefoneCommandHandler = edicaoTelefoneCommandHandler;
            this.exclusaoTelefoneCommandHandler = exclusaoTelefoneCommandHandler;
        }

        /// <summary>
        /// Telefones usuário 
        /// </summary>
        /// <param name="idUsuario">Dados do usuário</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ConsultaTelefoneResponse> Consulta([FromRoute] string idUsuario)
        {
            var query = new ConsultaTelefoneQuery(idUsuario);

            var retorno = this.consultaTelefoneQueryHandler.Handle(query);
            return this.TratarRetorno(retorno, this.consultaTelefoneQueryHandler);
        }

        /// <summary>
        /// Cadastrar telefone usuário
        /// </summary>
        /// <param name="idUsuario">Identificador do usuário</param>
        /// <param name="command">Dados do usuário</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<CadastroTelefoneResponse> Cadatrar([FromRoute] string idUsuario, [FromBody] CadastroTelefoneCommand command)
        {
            command.IdUsuario = idUsuario;

            var retorno = this.cadastroTelefoneCommandHandler.Handle(command);
            return this.TratarRetorno(retorno, this.cadastroTelefoneCommandHandler);
        }

        /// <summary>
        /// Editar telefone usuário
        /// </summary>
        /// <param name="id">Identificador do telefone</param>
        /// <param name="command">Dados do usuário</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<EdicaoTelefoneResponse> Editar([FromRoute] string id, [FromBody] EdicaoTelefoneCommand command)
        {
            command.Id = id;

            var retorno = this.edicaoTelefoneCommandHandler.Handle(command);
            return this.TratarRetorno(retorno, this.edicaoTelefoneCommandHandler);
        }

        /// <summary>
        /// Excluir telefone usuário
        /// </summary>
        /// <param name="id">Identificador do telefone</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<ExclusaoTelefoneResponse> Excluir([FromRoute] string id)
        {
            var command = new ExclusaoTelefoneCommand(id);

            var retorno = this.exclusaoTelefoneCommandHandler.Handle(command);
            return this.TratarRetorno(retorno, this.exclusaoTelefoneCommandHandler);
        }
    }
}
