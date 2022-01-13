using VendaZap.Usuarios.Dominio.Repositorios;
using System.Linq;
using VendaZap.Usuarios.Dominio.CasosDeUso.Chapas.Consultar;
using System.Collections.Generic;

namespace VendaZap.Usuarios.Dados.Repositorios
{
    public class ChapaRepository : IChapaRepository
    {
        private readonly UsuariosContext dbContext;

        public ChapaRepository(UsuariosContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool ExisteChapaPorId(string id)
        {
            var q = from c in this.dbContext.Chapas
                    where c.Id == id && c.Ativo
                    select c.Id;

            return q.FirstOrDefault() != null;
        }

        public List<ChapaDTO> ObterChapas()
        {
            var q = from c in this.dbContext.Chapas
                    select new ChapaDTO
                    {
                        Id = c.Id,
                        Nome = c.Nome
                    };

            return q.ToList();
        }

        public ChapaDTO ObterDadosChapaPorId(string id)
        {
            var q = from c in this.dbContext.Chapas
                    where c.Id == id && c.Ativo
                    select new ChapaDTO
                    {
                        Id = c.Id,
                        Nome = c.Nome
                    };

            return q.FirstOrDefault();
        }
    }
}
