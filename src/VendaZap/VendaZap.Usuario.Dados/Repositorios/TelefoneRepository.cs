using System.Collections.Generic;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Consultar;
using VendaZap.Usuarios.Dominio.Repositorios;
using System.Linq;
using VendaZap.Usuarios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace VendaZap.Usuarios.Dados.Repositorios
{
    public class TelefoneRepository: ITelefoneRepository
    {
        private readonly UsuariosContext dbContext;

        public TelefoneRepository(UsuariosContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Cadastrar(Telefone telefone)
        {
            this.dbContext.Telefones.Attach(telefone);
            this.dbContext.Entry(telefone).State = EntityState.Added;

            this.dbContext.SaveChanges();
        }

        public void Editar(Telefone telefone)
        {
            this.dbContext.Telefones.Attach(telefone);
            this.dbContext.Entry(telefone).State = EntityState.Modified;

            this.dbContext.SaveChanges();
        }

        public void Excluir(Telefone telefone)
        {
            this.dbContext.Telefones.Attach(telefone);
            this.dbContext.Entry(telefone).State = EntityState.Deleted;

            this.dbContext.SaveChanges();
        }

        public Telefone ObterTelefonePorId(string id)
        {
            return this.dbContext.Telefones.Where(x => x.Id == id).AsNoTracking().FirstOrDefault();
        }

        public List<TelefoneDTO> ObterTelefonesUsuarioPorIdUsuario(string idUsuario)
        {
            var q = from t in this.dbContext.Telefones
                    where t.IdUsuario == idUsuario
                    select new TelefoneDTO
                    {
                      Id = t.Id,
                      DDD= t.DDD,
                      Numero = t.Numero
                    };

            return q.ToList();
        }
    }
}
