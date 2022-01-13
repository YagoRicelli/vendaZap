using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using VendaZap.Comum.Dominio.Messages;

namespace VendaZap.Comum.Dominio
{

    public class DomainBaseItem : INotifiable
    {
        [NotMapped]
        public bool Invalid => this.Notifications.Any();

        [NotMapped]
        public bool Valid => !this.Invalid;

        [NotMapped]
        public List<Notification> Notifications { get; }

        [NotMapped]
        public string Message
        {
            get
            {
                return string.Join(" ", this.Notifications.Select(x => x.Message));
            }
        }

        [NotMapped]
        public List<string> Messages
        {
            get { return this.Notifications.Select(x => x.Message).ToList(); }
        }

        public DomainBaseItem()
        {
            this.Notifications = new List<Notification>();
        }

        public void AddNotification(string message)
        {
            this.Notifications.Add(new Notification(message));
        }

        public void AddNotification(string property, string message)
        {
            this.Notifications.Add(new Notification(property, message));
        }

        public void AddNotifications(List<Notification> notifications)
        {
            this.Notifications.AddRange(notifications);
        }

    }
}
