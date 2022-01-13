using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VendaZap.Comum.Dominio.Queries;
using VendaZap.Usuarios.Dominio.CasosDeUso.Chapas.Consultar;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Consultar;

namespace VendaZap.Api.Admin.Controllers
{
    [AllowAnonymous, Route("api/chapas")]
    public class Chapa : BaseController
    {
        private QueryHandler<ConsultaChapaQuery, ConsultaChapaResponse> consultaChapaQueryHandler;

        public Chapa(QueryHandler<ConsultaChapaQuery, ConsultaChapaResponse> consultaChapaQueryHandler)
        {
            this.consultaChapaQueryHandler = consultaChapaQueryHandler;
        }

        /// <summary>
        /// Obter chapas 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ConsultaTelefoneResponse> Consulta()
        {
            var query = new ConsultaChapaQuery();

            var retorno = this.consultaChapaQueryHandler.Handle(query);
            return this.TratarRetorno(retorno, this.consultaChapaQueryHandler);
        }
    }
}
