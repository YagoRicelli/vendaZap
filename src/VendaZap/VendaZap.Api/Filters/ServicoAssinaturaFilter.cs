using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using VendaZap.Comum.Configuracoes;

namespace VendaZap.Api.Filters
{
    public class ServicoAssinaturaFilter : IAuthorizationFilter
    {
        private const string ApiKeyHeaderName = "x-api-key";

        private readonly IOptions<ConfiguracaoAppSettings> configuracoes;

        public ServicoAssinaturaFilter(IOptions<ConfiguracaoAppSettings> configuracoes)
        {
            this.configuracoes = configuracoes;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var apiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            };

            if (!apiKey.Equals(this.configuracoes.Value.ServicoAssinaturaApiKey))
            {
                context.Result = new UnauthorizedResult();
                return;
            };
        }
    }
}
