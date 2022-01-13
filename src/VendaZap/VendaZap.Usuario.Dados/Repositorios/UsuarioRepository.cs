using VendaZap.Comum.Dominio.ValueObjects;
using VendaZap.Usuarios.Dominio.CasosDeUso.RealizarLogin;
using VendaZap.Usuarios.Dominio.Repositorios;
using System.Linq;
using VendaZap.Usuarios.Dominio.Enumeradores;
using VendaZap.Usuarios.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using VendaZap.Usuarios.Dominio.CasosDeUso.ConsultarPorId;
using VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.ConsultarPorId;
using System.Collections.Generic;

namespace VendaZap.Usuarios.Dados.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly UsuariosContext dbContext;

        public UsuarioRepository(UsuariosContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool ExisteUsuarioLider(string id)
        {
            var q = from u in this.dbContext.Usuarios
                    where u.Id == id && u.Tipo == ETipoUsuario.Lider
                    select u.Id;

            return q.FirstOrDefault() != null;
        }

        public bool JaExisteUsuarioCadastradoPorEmail(Email email)
        {
            var q = from u in this.dbContext.Usuarios
                    where u.Email == email
                    select u.Id;

            return q.FirstOrDefault() != null;
        }

        public Login ObterDadosLoginUsuarioPorEmail(Email email)
        {
            var q = from u in this.dbContext.Usuarios
                    where u.Email == email
                    select new Login
                    {
                        Id = u.Id,
                        Senha = u.Senha,
                        Tipo = u.Tipo
                    };

            return q.FirstOrDefault();
        }

        public void Cadastrar(Usuario usuario)
        {
            this.dbContext.Usuarios.Attach(usuario);
            this.dbContext.Entry(usuario).State = EntityState.Added;

            this.dbContext.SaveChanges();
        }

        public UsuarioDTO ObterUsuarioPorId(string id)
        {
            var q = from u in this.dbContext.Usuarios
                    where u.Id == id
                    select new UsuarioDTO
                    {
                        Id = u.Id,
                        Nome = u.Nome,
                        Sobrenome = u.Sobrenome,
                        IdUsuarioLider = u.IdUsuarioLider,
                        IdChapa = u.IdChapa,
                        Tipo = u.Tipo
                    };

            return q.FirstOrDefault();
        }

        public UsuarioLiderDTO ObterDadosUsuarioLiderPorId(string id)
        {
            var q = from u in this.dbContext.Usuarios
                    where u.Id == id && u.Tipo == ETipoUsuario.Lider
                    select new UsuarioLiderDTO
                    {
                        Id = u.Id,
                        Nome = u.Nome
                    };

            return q.FirstOrDefault();
        }

        public void Editar(Usuario usuario)
        {
            this.dbContext.Entry(usuario).State = EntityState.Modified;
            this.dbContext.SaveChanges();
        }

        public void Editar(List<Usuario> usuarios)
        {
            foreach (var usuario in usuarios)
            {
                this.dbContext.Entry(usuario).State = EntityState.Modified;
            }

            this.dbContext.SaveChanges();
        }

        public Usuario ObterUsuario(string id)
        {
            var q = from u in this.dbContext.Usuarios
                    where u.Id == id
                    select u;

            return q.AsNoTracking().FirstOrDefault();
        }

        public bool ExisteOutroUsuarioComEmailCadastrado(Email email, string id)
        {
            var q = from u in this.dbContext.Usuarios
                    where u.Email == email && u.Id != id
                    select u.Id;

            return q.FirstOrDefault() != null;
        }

        public bool ExistePorId(string id)
        {
            var q = from u in this.dbContext.Usuarios
                    where u.Id == id
                    select u.Id;

            return q.FirstOrDefault() != null;
        }

        public List<Usuario> ObterUsuariosVinculadosAoLiderPorIdLider(string idLider)
        {
            var q = from u in this.dbContext.Usuarios
                    where u.IdUsuarioLider == idLider
                    select u;

            return q.AsNoTracking().ToList();
        }

        public List<UsuarioDTO> ObterUsuariosLideres()
        {
            var q = from u in this.dbContext.Usuarios
                    where u.Tipo == ETipoUsuario.Lider
                    select new UsuarioDTO
                    {
                        Id = u.Id,
                        Nome = u.Nome,
                        Sobrenome = u.Sobrenome,
                        Ativo = u.Ativo
                    };

            return q.ToList();
        }
    }
}
