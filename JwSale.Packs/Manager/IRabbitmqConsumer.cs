using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Packs.Manager
{
    /// <summary>
    /// 消费者
    /// </summary>
    public interface  IRabbitmqConsumer: IRabbitmq
    {
        /// <summary>
        /// 订阅消息
        /// </summary>
        void Register();
    }
}
