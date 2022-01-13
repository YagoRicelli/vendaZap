namespace VendaZap.Comum.Dominio.Commands
{
    public abstract class Command : DomainBaseItem
    {
        public abstract bool Validar();
    }
}
