using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VendaZap.Comum.Dominio.Commands;
using VendaZap.Comum.Dominio.Queries;
using VendaZap.Usuarios.Dominio.CasosDeUso.Cadastrar;
using VendaZap.Usuarios.Dominio.CasosDeUso.ConsultarPorId;
using VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.ConsultarLideres;
using VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.Editar;
using VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.Excluir;

namespace VendaZap.Api.Admin.Controllers
{

    [AllowAnonymous, Route("api/usuarios")]
    public class UsuarioController : BaseController
    {
        private CommandHandler<CadastroUsuarioCommand, CadastroUsuarioResponse> cadastroUsuarioCommandHandler;
        private QueryHandler<ConsultaUsuarioPorIdQuery, ConsultaUsuarioPorIdResponse> consultaUsuarioPorIdQueryHandler;
        private CommandHandler<ExclusaoUsuarioCommand, ExclusaoUsuarioResponse> exclusaoUsuarioCommandHandler;
        private CommandHandler<EdicaoUsuarioCommand, EdicaoUsuarioResponse> edicaoUsuarioCommandHandler;
        private QueryHandler<ConsultaLideresQuery, ConsultaLideresResponse> consultaLideresQueryHandler;

        public UsuarioController(CommandHandler<CadastroUsuarioCommand, CadastroUsuarioResponse> cadastroUsuarioCommandHandler, QueryHandler<ConsultaUsuarioPorIdQuery, ConsultaUsuarioPorIdResponse> consultaUsuarioPorIdQueryHandler, CommandHandler<ExclusaoUsuarioCommand, ExclusaoUsuarioResponse> exclusaoUsuarioCommandHandler, CommandHandler<EdicaoUsuarioCommand, EdicaoUsuarioResponse> edicaoUsuarioCommandHandler, QueryHandler<ConsultaLideresQuery, ConsultaLideresResponse> consultaLideresQueryHandler = null)
        {
            this.cadastroUsuarioCommandHandler = cadastroUsuarioCommandHandler;
            this.consultaUsuarioPorIdQueryHandler = consultaUsuarioPorIdQueryHandler;
            this.exclusaoUsuarioCommandHandler = exclusaoUsuarioCommandHandler;
            this.edicaoUsuarioCommandHandler = edicaoUsuarioCommandHandler;
            this.consultaLideresQueryHandler = consultaLideresQueryHandler;
        }

        /// <summary>
        /// Cadastrar usuário
        /// </summary>
        /// <param name="command">Dados do usuário</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(CadastroUsuarioResponse), StatusCodes.Status200OK)]
        public ActionResult<CadastroUsuarioResponse> Login([FromBody] CadastroUsuarioCommand command)
        {
            var retorno = this.cadastroUsuarioCommandHandler.Handle(command);
            return this.TratarRetorno(retorno, this.cadastroUsuarioCommandHandler);
        }

        /// <summary>
        /// Obter dados usuário 
        /// </summary>
        /// <param name="id">Identificador usuário</param>
        /// <returns></returns>
        [HttpGet("{id}")]

        public ActionResult<ConsultaUsuarioPorIdResponse> Consultar([FromRoute] string id)
        {
            var query = new ConsultaUsuarioPorIdQuery(id);

            var retorno = this.consultaUsuarioPorIdQueryHandler.Handle(query);
            return this.TratarRetorno(retorno, this.consultaUsuarioPorIdQueryHandler);
        }

        /// <summary>
        /// Obter usuários lideres 
        /// </summary>
        /// <returns></returns>
        [HttpGet("lideres")]

        public ActionResult<ConsultaLideresResponse> ConsultarLideres()
        {
            var query = new ConsultaLideresQuery();

            var retorno = this.consultaLideresQueryHandler.Handle(query);
            return this.TratarRetorno(retorno, this.consultaLideresQueryHandler);
        }

        /// <summary>
        /// Excluir usuário
        /// </summary>
        /// <param name="id">Identificador usuário</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<ExclusaoUsuarioResponse> Excluir([FromRoute] string id)
        {
            var command = new ExclusaoUsuarioCommand(id);

            var retorno = this.exclusaoUsuarioCommandHandler.Handle(command);
            return this.TratarRetorno(retorno, this.exclusaoUsuarioCommandHandler);
        }

        /// <summary>
        /// Editar usuário
        /// </summary>
        /// <param name="id">Identificador usuário</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<EdicaoUsuarioResponse> Editar([FromRoute] string id, [FromBody] EdicaoUsuarioCommand command)
        {
            command.Id = id;

            var retorno = this.edicaoUsuarioCommandHandler.Handle(command);
            return this.TratarRetorno(retorno, this.edicaoUsuarioCommandHandler);
        }
    }
}
