using JwSale.Api.Const;
using JwSale.Packs.Manager;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Api.Events
{
    public class RefreshWxInfoResovePublisher : IRabbitmqPublisher
    {
        public string Resove(IModel channel)
        {
            //声明一个队列
 
            channel.QueueDeclare(QueueConst.RefreshWxInfoName, false, false, false, null);
            return QueueConst.RefreshWxInfoName;
        }
    }
}
