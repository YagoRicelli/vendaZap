using System.Collections.Generic;
using VendaZap.Comum.Dominio.BusinessRule;
using VendaZap.Comum.Dominio.Messages;
using VendaZap.Comum.Dominio.Utils;

namespace VendaZap.Comum.Dominio.ValueObjects
{
    public class Email : ValueObject
    {
        private readonly string email;

        public Email(string email)
        {
            this.email = email;

            this.Validar();
        }

        private void Validar()
        {
            if (EmailUtil.IsInvalid(this.email))
            {
                throw new BusinessRuleException(Mensagem.EmailInvalido);
            }
        }

        public override string ToString()
        {
            return email;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.email;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(string))
            {
                return this.email == obj.ToString();
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.email == ((Email)obj).email;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class NullEmail : Email
    {
        public NullEmail() : base("")
        {
            throw new BusinessRuleException(Mensagem.EmailNaoInformado);
        }
    }
}
