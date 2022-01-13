using System.Collections.Generic;
using VendaZap.Usuarios.Dominio.CasosDeUso.Chapas.Consultar;

namespace VendaZap.Usuarios.Dominio.Repositorios
{
    public interface IChapaRepository
    {
        bool ExisteChapaPorId(string id);
        ChapaDTO ObterDadosChapaPorId(string id);
        List<ChapaDTO> ObterChapas();
    }
}
