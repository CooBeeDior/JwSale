using JwSale.Api.Const;
using JwSale.Api.Const.JwSale.Api.Const;
using JwSale.Packs.Manager;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwSale.Api.Events
{
    /// <summary>
    /// 发布者
    /// </summary>
    public class TestPublisher : IRabbitmqPublisher
    {
        public string Resove(IModel channel)
        {
            //声明一个队列

            channel.QueueDeclare(QueueConst.TESTQUEUENAME, false, false, false, null);
            return QueueConst.TESTQUEUENAME;
        }
    }
}