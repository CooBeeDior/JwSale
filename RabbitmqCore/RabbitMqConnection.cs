using JwSale.Util.Extensions;
using RabbitMQ.Client;

namespace RabbitmqCore
{
    public class RabbitMqConnection : IRabbitMqConnection
    {
        private readonly RabbitmqOptions _rabbitmqOptions;
        public RabbitMqConnection(RabbitmqOptions rabbitmqOptions)
        {
            _rabbitmqOptions = rabbitmqOptions;

        }

        /// <summary>
        /// 获取一个链接
        /// </summary>
        /// <returns></returns>
        public IConnection GetConnection()
        {
            if (_rabbitmqOptions != null && !_rabbitmqOptions.HostUrl.IsNullOrWhiteSpace())
            {

                //创建连接工厂
                IConnectionFactory factory = new ConnectionFactory
                {
                    UserName = _rabbitmqOptions.UserName,//用户名
                    Password = _rabbitmqOptions.Password,//密码
                    HostName = _rabbitmqOptions.HostUrl//rabbitmq ip
                };
                //创建连接
                var connection = factory.CreateConnection();
                return connection;
            }
            return null;
        }
    }
}
