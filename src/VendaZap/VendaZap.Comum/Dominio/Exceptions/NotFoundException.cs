using System;

namespace VendaZap.Comum.Dominio.BusinessRule
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {

        }
    }
}
