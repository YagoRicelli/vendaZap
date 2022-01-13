using VendaZap.Comum.Dominio.Commands;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.RealizarLogin
{
    public class RealizaLoginCommand : Command
    {
        public RealizaLoginCommand(string email, string senha)
        {
            this.Email = email;
            this.Senha = senha;

            this.Validar();
        }

        public RealizaLoginCommand()
        {
        }

        public string Email { get; set; }
        public string Senha { get; set; }

        public override bool Validar()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                this.AddNotification(Mensagem.RealizarLogin.EmailObrigatorio);
            }

            if (string.IsNullOrEmpty(this.Senha))
            {
                this.AddNotification(Mensagem.RealizarLogin.SenhaObrigatoria);
            }

            return this.Valid;
        }
    }
}
