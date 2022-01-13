using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendaZap.Comum.Dominio.Messages
{
    public interface INotifiable
    {
        bool Invalid { get; }

        bool Valid { get; }

        void AddNotification(string message);

        void AddNotification(string property, string message);

        void AddNotifications(List<Notification> notifications);
    }
}
