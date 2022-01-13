using VendaZap.Comum.Dominio.Queries;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Chapas.Consultar
{
    public class ConsultaChapaQuery : Query
    {
        public override bool Validar()
        {
            return this.Valid;
        }
    }
}
