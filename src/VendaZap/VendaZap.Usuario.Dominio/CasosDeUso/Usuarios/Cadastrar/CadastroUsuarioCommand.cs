using System;
using VendaZap.Comum.Dominio.Commands;
using VendaZap.Usuarios.Dominio.Enumeradores;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Cadastrar
{
    public class CadastroUsuarioCommand : Command
    {
        public CadastroUsuarioCommand(string nome, string sobrenome, string email, string senha, string idUsuarioLider, string idChapa, ETipoUsuario tipo)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            Senha = senha;
            IdUsuarioLider = idUsuarioLider;
            IdChapa = idChapa;
            Tipo = tipo;

            this.Validar();
        }

        public CadastroUsuarioCommand()
        {

        }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string IdUsuarioLider { get; set; }
        public string IdChapa { get; set; }
        public ETipoUsuario Tipo { get; set; }

        public override bool Validar()
        {
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

            if (string.IsNullOrEmpty(this.Senha))
            {
                this.AddNotification(Mensagem.Usuario.SenhaObrigatoria);
            }

            if (string.IsNullOrEmpty(this.IdChapa))
            {
                this.AddNotification(Mensagem.Chapa.IdObrigatorio);
            }

            return this.Valid;
        }
    }
}
