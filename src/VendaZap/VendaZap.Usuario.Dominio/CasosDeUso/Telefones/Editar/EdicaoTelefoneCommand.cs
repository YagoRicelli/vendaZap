using VendaZap.Comum.Dominio.Commands;

namespace VendaZap.Usuarios.Dominio.CasosDeUso.Telefones.Editar
{
    public class EdicaoTelefoneCommand : Command
    {
        public EdicaoTelefoneCommand(string id, int ddd, int numero)
        {
            DDD = ddd;
            Numero = numero;
            Id = id;

            this.Validar();
        }

        public EdicaoTelefoneCommand()
        {

        }

        public string Id { get; set; }
        public int DDD { get; set; }
        public int Numero { get; set; }

        public override bool Validar()
        {
            if (string.IsNullOrEmpty(this.Id))
            {
                this.AddNotification(Mensagem.Telefone.IdObrigatorio);
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
