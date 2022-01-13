using VendaZap.Comum.Dominio.Commands;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Cadastrar
{
    public class CadastroTelefoneCommand : Command
    {
        public CadastroTelefoneCommand(string idUsuario, int dDD, int numero)
        {
            IdUsuario = idUsuario;
            DDD = dDD;
            Numero = numero;

            this.Validar();
        }

        public CadastroTelefoneCommand()
        {

        }

        public string IdUsuario { get; set; }
        public int DDD { get; set; }
        public int Numero { get; set; }

        public override bool Validar()
        {
            if (string.IsNullOrEmpty(this.IdUsuario))
            {
                this.AddNotification(Mensagem.Usuario.IdObrigatorio);
            }

            if (DDD < 1)
            {
                this.AddNotification(Mensagem.Telefone.DDDObrigatorio);
            }

            if (Numero < 1)
            {
                this.AddNotification(Mensagem.Telefone.NumerObrigatorio);
            }

            return this.Valid;
        }
    }
}
