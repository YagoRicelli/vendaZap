using System.Linq;
using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.Queries;
using VendaZap.Usuarios.Dominio.Repositorios;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Consultar
{
    public class ConsultaTelefoneQueryHandler : QueryHandler<ConsultaTelefoneQuery, ConsultaTelefoneResponse>
    {
        private readonly ITelefoneRepository telefoneRepository;

        public ConsultaTelefoneQueryHandler(ITelefoneRepository telefoneRepository)
        {
            this.telefoneRepository = telefoneRepository;
        }

        public override ConsultaTelefoneResponse Handle(ConsultaTelefoneQuery query)
        {
            if (!query.Validar())
            {
                this.AddNotifications(query.Notifications);
                return null;
            }

            var telefones = this.telefoneRepository.ObterTelefonesUsuarioPorIdUsuario(query.IdUsuario);
            if (telefones == null || !telefones.Any())
            {
                throw new NotFoundException(Mensagem.Telefone.UsuarioNãoPossuiNenhumTelefoneCadastrado);
            }

            return new ConsultaTelefoneResponse(telefones);
        }
    }
}
