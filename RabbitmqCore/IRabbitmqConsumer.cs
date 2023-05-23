using JwSale.Util.Extensions;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitmqCore
{
    /// <summary>
    /// 消费者
    /// </summary>
    public interface IRabbitmqConsumer : IRabbitmq 
    {
        /// <summary>
        /// 订阅消息
        /// </summary>
        void Subscripe();


       
    }
}
