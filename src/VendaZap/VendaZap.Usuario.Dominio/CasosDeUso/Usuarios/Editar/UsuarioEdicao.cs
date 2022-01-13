using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.ValueObjects;
using VendaZap.Usuarios.Dominio.Entidades;
using VendaZap.Usuarios.Dominio.Enumeradores;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.Editar
{
    public class UsuarioEdicao : Usuario
    {
        public UsuarioEdicao(string id, string nome, string sobrenome, string email, Senha senha, string idUsuarioLider, string idChapa, ETipoUsuario tipo) :
                base(nome,sobrenome, email, idUsuarioLider, senha, idChapa, tipo)
        {
            this.Id = id;

            this.Validar();
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(this.Id))
            {
                throw new BusinessRuleException(Mensagem.Usuario.IdObrigatorio);
            }

            if (string.IsNullOrEmpty(this.Nome.ToString()))
            {
                throw new BusinessRuleException(Mensagem.Usuario.NomeUsuarioObrigatorio);
            }

            if (string.IsNullOrEmpty(this.Sobrenome.ToString()))
            {
                throw new BusinessRuleException(Mensagem.Usuario.SobrenomeUsuarioObrigatorio);
            }

            if (string.IsNullOrEmpty(this.Email.ToString()))
            {
                throw new BusinessRuleException(Mensagem.Usuario.EmailObrigatorio);
            }

            if (string.IsNullOrEmpty(this.IdChapa.ToString()))
            {
                throw new BusinessRuleException(Mensagem.Chapa.IdObrigatorio);
            }
        }
    }
}
