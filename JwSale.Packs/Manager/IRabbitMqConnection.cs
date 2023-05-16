using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Packs.Manager
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
