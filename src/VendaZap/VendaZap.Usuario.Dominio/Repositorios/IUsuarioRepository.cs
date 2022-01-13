using System.Collections.Generic;
using VendaZap.Comum.Dominio.ValueObjects;
using VendaZap.Usuarios.Dominio.CasosDeUso.ConsultarPorId;
using VendaZap.Usuarios.Dominio.CasosDeUso.RealizarLogin;
using VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.ConsultarPorId;
using VendaZap.Usuarios.Dominio.Entidades;

namespace VendaZap.Usuarios.Dominio.Repositorios
{
    public interface IUsuarioRepository
    {
        Login ObterDadosLoginUsuarioPorEmail(Email email);
        bool ExisteUsuarioLider(string id);
        bool JaExisteUsuarioCadastradoPorEmail(Email email);
        void Cadastrar(Usuario usuario);
        void Editar(Usuario usuario);
        Usuario ObterUsuario(string id);
        UsuarioDTO ObterUsuarioPorId(string id);
        UsuarioLiderDTO ObterDadosUsuarioLiderPorId(string id);
        bool ExisteOutroUsuarioComEmailCadastrado(Email email, string id);
        bool ExistePorId(string id);
        List<Usuario> ObterUsuariosVinculadosAoLiderPorIdLider(string idLider);
        void Editar(List<Usuario> usuarios);
        List<UsuarioDTO> ObterUsuariosLideres();
    }
}
