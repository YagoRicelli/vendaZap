using System;

namespace VendaZap.Comum.Dominio.BusinessRule
{
    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message)
        {

        }
    }
}
