using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitmqCore
{
    public interface IRabbitMqConnection
    {
        /// <summary>
        /// 获取一个链接
        /// </summary>
        /// <returns></returns>
        IConnection GetConnection();
    }
}
