using System;

namespace VendaZap.Comum.Dominio.BusinessRule
{
    public class BusinessRuleException : Exception
    {
        public BusinessRuleException(string message) : base(message)
        {

        }
    }
}
