using System;
using VendaZap.Comum.Dominio;
using VendaZap.Comum.Dominio.BusinessRule;

namespace VendaZap.Usuarios.Dominio.Entidades
{
    public class Telefone : DomainEntity
    {
        public string Id { get; protected set; }
        public int DDD { get; protected set; }
        public int Numero { get; protected set; }
        public string IdUsuario { get; protected set; }
        public Usuario Usuario { get; protected set; }

        public Telefone()
        {

        }
        public Telefone(int ddd, int numero, string idUsuario)
        {
            this.Id = Guid.NewGuid().ToString();
            DDD = ddd;
            Numero = numero;
            IdUsuario = idUsuario;

            this.Validar();
        }

        private void Validar()
        {
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
