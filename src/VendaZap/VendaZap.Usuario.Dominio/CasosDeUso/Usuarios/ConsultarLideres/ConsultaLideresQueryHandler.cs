using VendaZap.Comum.Dominio.Queries;
using VendaZap.Usuarios.Dominio.Repositorios;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.ConsultarLideres
{
    public class ConsultaLideresQueryHandler : QueryHandler<ConsultaLideresQuery, ConsultaLideresResponse>
    {
        private readonly IUsuarioRepository usuarioRepository;

        public ConsultaLideresQueryHandler(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public override ConsultaLideresResponse Handle(ConsultaLideresQuery query)
        {
            if (!query.Validar())
            {
                this.AddNotifications(query.Notifications);
                return null;
            }

            var lideres = this.usuarioRepository.ObterUsuariosLideres();

            return new ConsultaLideresResponse(lideres);
        }
    }
}
