using JwSale.Packs.Options;
using JwSale.Util.Extensions;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace JwSale.Packs.Manager
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        private readonly JwSaleOptions _jwSaleOptions;
        public RabbitMqConnection(JwSaleOptions jwSaleOptions)
        {
            _jwSaleOptions = jwSaleOptions;

        }

        /// <summary>
        /// 获取一个链接
        /// </summary>
        /// <returns></returns>
        public IConnection GetConnection()
        {
            if (_jwSaleOptions.Rabbitmq != null && !_jwSaleOptions.Rabbitmq.HostUrl.IsNullOrWhiteSpace())
            {

                //创建连接工厂
                IConnectionFactory factory = new ConnectionFactory
                {
                    UserName = _jwSaleOptions.Rabbitmq.UserName,//用户名
                    Password = _jwSaleOptions.Rabbitmq.Password,//密码
                    HostName = _jwSaleOptions.Rabbitmq.HostUrl//rabbitmq ip
                };
                //创建连接
                var connection = factory.CreateConnection();
                return connection;
            }
            return null;
        }
    }
}
