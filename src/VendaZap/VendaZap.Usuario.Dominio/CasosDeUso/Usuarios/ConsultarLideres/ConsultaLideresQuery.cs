using VendaZap.Comum.Dominio.Queries;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.ConsultarLideres
{
    public class ConsultaLideresQuery : Query
    {
        public override bool Validar()
        {
            return this.Valid;
        }
    }
}
