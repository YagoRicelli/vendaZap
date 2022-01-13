using System;
using VendaZap.Comum.Dominio.Queries;
using VendaZap.Usuarios.Dominio.Repositorios;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Chapas.Consultar
{
    public class ConsultaChapaQueryHandler : QueryHandler<ConsultaChapaQuery, ConsultaChapaResponse>
    {
        private readonly IChapaRepository repository;

        public ConsultaChapaQueryHandler(IChapaRepository repository)
        {
            this.repository = repository;
        }

        public override ConsultaChapaResponse Handle(ConsultaChapaQuery query)
        {
            if (!query.Validar())
            {
                this.AddNotifications(query.Notifications);
                return null;
            }

            var chapas = this.repository.ObterChapas();

            return new ConsultaChapaResponse(chapas);
        }
    }
}
