namespace VendaZap.Comum.Dominio.Queries
{
    public abstract class Query : DomainBaseItem
    {
        public abstract bool Validar();
    }
}
