using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Packs.Manager
{
    public interface  IRabbitmqConsumer: IRabbitmq
    {
        void Register();
    }
}
