using System.Collections.Generic;
using VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Consultar;
using VendaZap.Usuarios.Dominio.Entidades;

namespace VendaZap.Usuarios.Dominio.Repositorios
{
    public interface ITelefoneRepository
    {
        void Editar(Telefone telefone);
        void Cadastrar(Telefone telefone);
        void Excluir(Telefone telefone);
        List<TelefoneDTO> ObterTelefonesUsuarioPorIdUsuario(string idUsuario);
        Telefone ObterTelefonePorId(string id);
    }
}
