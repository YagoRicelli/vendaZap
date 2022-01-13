using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Usuarios.Dominio.Entidades;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Editar
{
    internal class TelefoneEdicao : Telefone
    {
        public TelefoneEdicao(string id, int ddd, int numero, string idUsuario) :
                base(ddd, numero, idUsuario)
        {
            this.Id = id;

            this.Validar();
        }

        private void Validar()
        {
            if (string.IsNullOrEmpty(this.Id))
            {
                throw new BusinessRuleException(Mensagem.Usuario.IdObrigatorio);
            }

            if (DDD < 1)
            {
                throw new BusinessRuleException(Mensagem.Telefone.DDDObrigatorio);
            }

            if (Numero < 1)
            {
                throw new BusinessRuleException(Mensagem.Telefone.NumerObrigatorio);
            }

            if (string.IsNullOrEmpty(this.IdUsuario))
            {
                throw new BusinessRuleException(Mensagem.Telefone.IdUsuarioObrigatorio);
            }
        }
    }
}