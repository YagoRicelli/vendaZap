using System;
using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.Queries;
using VendaZap.Usuarios.Dominio.Repositorios;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.ConsultarPorId
{
    public class ConsultaUsuarioPorIdQueryHandler : QueryHandler<ConsultaUsuarioPorIdQuery, ConsultaUsuarioPorIdResponse>
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ITelefoneRepository telefoneRepository;
        private readonly IChapaRepository chapaRepository;

        public ConsultaUsuarioPorIdQueryHandler(IUsuarioRepository usuarioRepository, ITelefoneRepository telefoneRepository, IChapaRepository chapaRepository)
        {
            this.usuarioRepository = usuarioRepository;
            this.telefoneRepository = telefoneRepository;
            this.chapaRepository = chapaRepository;
        }

        public override ConsultaUsuarioPorIdResponse Handle(ConsultaUsuarioPorIdQuery query)
        {
            if (!query.Validar())
            {
                this.AddNotifications(query.Notifications);
                return null;
            }

            var usuario = this.usuarioRepository.ObterUsuarioPorId(query.Id);
            if (usuario == null)
            {
                throw new NotFoundException(Mensagem.Usuario.NaoEnconrtrado);
            }

            this.ObterTelefonesUsuario(usuario);
            this.ObterDetalhesUsuarioLider(usuario);
            this.ObterDetalhesChapa(usuario);

            return new ConsultaUsuarioPorIdResponse(usuario);
        }

        private void ObterTelefonesUsuario(UsuarioDTO usuario)
        {
            usuario.Telefones = this.telefoneRepository.ObterTelefonesUsuarioPorIdUsuario(usuario.Id);
        }

        private void ObterDetalhesUsuarioLider(UsuarioDTO usuario)
        {
            if (!string.IsNullOrEmpty(usuario.IdUsuarioLider))
            {
                usuario.Lider = this.usuarioRepository.ObterDadosUsuarioLiderPorId(usuario.IdUsuarioLider);
            }
        }

        private void ObterDetalhesChapa(UsuarioDTO usuario)
        {
            usuario.Chapa = this.chapaRepository.ObterDadosChapaPorId(usuario.IdChapa);
        }
    }
}
