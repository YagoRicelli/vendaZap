using System.ComponentModel.DataAnnotations.Schema;

namespace VendaZap.Comum.Dominio.Messages
{
    public class Notification
    {
        private string property;

        [NotMapped]
        public string Message { get; private set; }

        public Notification(string message)
        {
            this.Message = message;
        }

        public Notification(string property, string message)
        {
            this.property = property;
            this.Message = message;
        }
    }
}
