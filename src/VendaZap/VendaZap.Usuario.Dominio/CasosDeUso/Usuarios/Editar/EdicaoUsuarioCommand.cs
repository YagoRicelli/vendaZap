using System;
using VendaZap.Comum.Dominio.Commands;
using VendaZap.Usuarios.Dominio.Enumeradores;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Usuarios.Editar
{
    public class EdicaoUsuarioCommand : Command
    {
        public EdicaoUsuarioCommand(string id, string nome, string sobrenome, string email, string idUsuarioLider, string idChapa, ETipoUsuario tipo)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            IdUsuarioLider = idUsuarioLider;
            IdChapa = idChapa;
            Tipo = tipo;

            this.Validar();
        }

        public EdicaoUsuarioCommand()
        {

        }
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string IdUsuarioLider { get; set; }
        public string IdChapa { get; set; }
        public ETipoUsuario Tipo { get; set; }

        public override bool Validar()
        {
            if (string.IsNullOrEmpty(this.Id))
            {
                this.AddNotification(Mensagem.Usuario.IdObrigatorio);
            }

            if (string.IsNullOrEmpty(this.Email))
            {
                this.AddNotification(Mensagem.Usuario.EmailObrigatorio);
            }

            if (string.IsNullOrEmpty(this.Nome))
            {
                this.AddNotification(Mensagem.Usuario.NomeUsuarioObrigatorio);
            }

            if (string.IsNullOrEmpty(this.Sobrenome))
            {
                this.AddNotification(Mensagem.Usuario.SobrenomeUsuarioObrigatorio);
            }

            if (string.IsNullOrEmpty(this.IdChapa))
            {
                this.AddNotification(Mensagem.Chapa.IdObrigatorio);
            }

            return this.Valid;
        }
    }
}
